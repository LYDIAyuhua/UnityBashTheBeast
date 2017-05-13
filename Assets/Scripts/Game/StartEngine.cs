using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEngine : MonoBehaviour {

	public Dice dice;
	public Sprite clicked;

	public void rollTheDice(){
		
		dice.rollTheDice ();

		Debug.Log ("Clicked");

	}
	void Start(){
		dice = GameObject.FindGameObjectWithTag ("Dice").GetComponent<Dice> ();
	}
		
}
