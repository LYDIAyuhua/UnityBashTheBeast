using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

	public int minDiceRuns = 1;					// how long the dices roll
	public int maxDiceRuns = 6;	
	public int diceNum;
	public bool isRolled = false;
	public Sprite[] diceSides; 	// a Dice has 6 Sides

	// Use this for initialization
	void Start () {
		diceNum = 1;
	}

	public void rollTheDice(){
		diceNum = Random.Range (minDiceRuns, maxDiceRuns);
		Debug.Log ("DICE: diceNum="+diceNum);
		isRolled = true;
	}

	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().sprite = diceSides[diceNum-1];
	}
}
