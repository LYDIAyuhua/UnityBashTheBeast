using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private GameController gc;
	public int ID;
	public string Name;
	public bool HasFinished;
	public int CurrentFieldID;
	public Sprite[] cars;
	public Sprite[] angles;
	public Vector2 track;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = cars[ID];
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		track = new Vector2 (0, 20);
	}

	// Update is called once per frame
	void Update () {

	}

	public void Action(){
		if (gc.inAction) {
			// on a pop-up menu or sth, select a track id [0-3] (inclusive) and provide a public variable that we can get
			if (gc.field[CurrentFieldID].Type == FieldType.Action && gc.field [CurrentFieldID].Action == ActionType.GoToField) {
				track = gc.tracks [0];
				// mark inAction as false after you are done with the action
				Move((int)track.x-CurrentFieldID);
			} 
			gc.inAction = false;
		}
	}

	public void Move(int dicedNumber){
		if (HasFinished)
			gc.NextPlayer ();
		else
			StartCoroutine(moveForwards(dicedNumber));
	}

	// Move the current Player to the target Position field by field
	IEnumerator moveForwards(int dicedNumber)
	{
		gc.isGamerMoving = true;

		// get the field the player is currently on
		int currentField = CurrentFieldID;


		if (currentField + dicedNumber <= track.y)
		{
			// if diced a 4 loop 4 times
			for(int i = 0; i < dicedNumber; i++)
			{
				//if(moveSound != null)
				//	audio.PlayOneShot(moveSound);

				float t = 0f;

				// increase the start- and endposition each iteration
				// startposition represents the field the player is on
				// endposition is always one field ahead of the players field
				// to make it look like the player moves one field at the time
				Vector3 startPosition = gc.field[(currentField + i)].transform.position + gc.playerSlots[ID];
				Vector3 endPosition = gc.field[(currentField + i + 1)].transform.position + gc.playerSlots[ID];

				float startZ = gc.field [(currentField + i)].transform.rotation.z;
				float endZ = gc.field [(currentField + i + 1)].transform.rotation.z;

				Debug.Log ("Start R.z="+startZ);
				Debug.Log ("End R.z="+endZ);

				// lerp to the next field
				while(t < 1f)
				{
					t += Time.deltaTime * 4f;

					transform.position = Vector3.Lerp (startPosition, endPosition, t);
					Debug.Log ("Rotate angle z="+(endZ-startZ));
					transform.Rotate (0,0,(endZ-startZ));
					yield return null;
				}
				yield return null;
			}
			// update the players current field
			CurrentFieldID = currentField + dicedNumber;
			//move to the hospital and need to pop up menu

				
			if (gc.field[CurrentFieldID].Type == FieldType.Finish)
			{
				HasFinished = true;
				gc.winner.Add (this);

				if(gc.stopWhenFirstPlayerHasFinished)
				{
					gc.isGameOver = true;
				}
				else
				{
					gc.isGameOver = gc.IsGameOver();
				}
				yield return new WaitForSeconds(0.15f);
			}

			// this player has finished moving
			gc.NextPlayer ();
		}

		// wait a little
		yield return new WaitForSeconds(0.1f);
		gc.isGamerMoving = false;
		gc.waitForYourTurn = false;

		yield return 0;
	}
}
