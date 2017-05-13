using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour {

	[Range(1, 4)]
	public int playerCount;

	// the 4 Player Positions on the Fields
	public Vector3[] playerSlots = new Vector3[]
	{
		new Vector3(-0.3f,  0.3f), // upper left
		new Vector3( 0.3f,  0.3f), // upper right
		new Vector3(-0.3f, -0.3f), // lower left
		new Vector3( 0.3f, -0.3f)  // lower right
	};

	// Vector3 playerBaseRotation = new Vector3(0,180,0);

	public int currentPlayer;
	// int selectedPlayer = -1;  							// for the Player selection Screen if one can choose a Player to send back to start
	// int totalDicedNumber = 0; 							// the total number of all Dices
	// int dicesFinished = 0; 								// this have to match dice.Count before the Player can Move
	public Dice dice;
	//List<Dice> dice = new List<Dice>();					// a List holding all the Dices
	public List<Player> player = new List<Player>();			// a List holding all the Players
	public List<Field> field = new List<Field>();				// a List holding all Fields
	public List<Player> winner = new List<Player>();

	// a list of Vector2 ==> Vertor2(start,finish) of each track
	public Vector2[] tracks = new Vector2[]
	{
		new Vector2(21,45),
		new Vector2(46,70)
	};

	public bool stopWhenFirstPlayerHasFinished = false;	// should the Game stop when the first Player reaches the finish?
	// public bool diceAgainWhenDicedSix = true;			// can the player dice again when he diced a six?

	public bool waitForYourTurn = false;						// dont allow input until its your turn
	public bool isGamerMoving = false;							// dont allow input as long as a player is moving
	public bool isSpawningPlayers = false;						// dont allow input as long as the players are spawning
	public bool isGameOver = false;							// is the Game Over yet?
	public bool inAction = false;



	// Use this for initialization
	void Start () {
		StartCoroutine(reset ());
	}


	// helper functions

	// reset a game
	IEnumerator reset()
	{

		waitForYourTurn = true;

		// search all field Objects and sort by id
		field.Clear();

		dice = GameObject.FindGameObjectWithTag ("Dice").GetComponent<Dice> ();
		yield return null;

		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Field"))
		{
			field.Add (g.GetComponent<Field>());

			yield return null;
		}
		field.Sort((a, b) => a.ID.CompareTo(b.ID));

		currentPlayer = Random.Range (0, playerCount);

		GameObject.FindGameObjectWithTag ("Interior").GetComponent<Interior> ().interiorID = currentPlayer;

		SpawnPlayer();

		isGamerMoving = false;

		while(isSpawningPlayers)
			yield return null;

		yield return new WaitForSeconds(1f);
		waitForYourTurn = false;

		yield return 0;
	}

	// Remove old Players and Start Coroutine 'spawnPlayer'
	// normally called at the beginning of the Game or when resetting the current Scene
	void SpawnPlayer()
	{
		player.Clear();
		while(GameObject.FindGameObjectWithTag("Player") != null)
		{
			DestroyImmediate(GameObject.FindGameObjectWithTag("Player"));
		}

		StartCoroutine(spawnPlayer());
	}

	// Spawns the players and moves them to the Start field.
	IEnumerator spawnPlayer()
	{
		isSpawningPlayers = true;


		// spawn the players
		for (int i = 0; i < playerCount; ++i) {
			float t = 0f;

			// create a list of players in menu and don't destroy on load 
			GameObject gamer = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
			gamer.name = "Player_" + (i + 1); // Gameobjects name doesn't really matter
			gamer.tag = "Player";
			gamer.GetComponent<Player> ().ID = i;
			gamer.GetComponent<Player> ().Name = "Player_"+(i+1);
			gamer.GetComponent<Player> ().HasFinished = false;
			gamer.GetComponent<Player> ().CurrentFieldID = 0;
			Vector3 startPosition = gamer.transform.position;
			Vector3 endPosition = field[0].transform.position + playerSlots[i];

			// lerp the player to the starting field
			while(t < 1f)
			{
				t += Time.deltaTime * 4f;

				gamer.transform.position = Vector3.Lerp(startPosition, endPosition, t);

				yield return null;
			}

			// add to players list
			player.Add(gamer.GetComponent<Player>());

			yield return null;
		}

		isSpawningPlayers = false;

		yield return 0;
	}


	
	// Update is called once per frame
	void Update () {
		if (isGameOver)
			return;

		// dont allow any input as long as a player is moving
		if (isGamerMoving)
			return;

		// dont allow any input until its your turn
		if (waitForYourTurn)
			return;

		// don't allow anyone move when a player is in action (monster card)
		if (inAction) {
			StartCoroutine(performAction ());
			return;
		}
			

		GameObject.FindGameObjectWithTag ("Interior").GetComponent<Interior> ().interiorID = currentPlayer;

		Debug.Log("GC: Player="+currentPlayer+" isRolled="+dice.isRolled);
		if (player.Count > 0 && dice.isRolled){
			dice.isRolled = false;
			StartCoroutine(playersTurn());
		}

	}

	IEnumerator performAction() {
		player [currentPlayer].Action ();
		yield return new WaitForSeconds(0.15f);
	}


	/// <summary>
	/// Players turn.
	/// First roll the dice, after that move the player to the target Field
	/// This Method is called e.g. after the current player pushes a Button or Presses a Key
	/// </summary>
	IEnumerator playersTurn()
	{
		waitForYourTurn = true;

		// Move to the target field
		player[currentPlayer].Move(dice.diceNum);
		inAction = true;
		yield return new WaitForSeconds(0.15f);
	}




	public bool IsGameOver()
	{
		foreach(Player g in player)
		{
			if (!g.HasFinished)
				return false;
		}

		return true;
	}

	// Simply swaps to the next Player
	public void NextPlayer()
	{
		if(isGameOver)
			return;

		currentPlayer++;
		if(currentPlayer >= playerCount)
		{
			currentPlayer = 0;
		}

		CheckNextPlayerHasFinished();

	}

	public void CheckNextPlayerHasFinished()
	{
		if(player[currentPlayer].HasFinished)
		{
			currentPlayer++;
		}

		if(currentPlayer >= playerCount)
		{
			currentPlayer = 0;
		}
	}



}
