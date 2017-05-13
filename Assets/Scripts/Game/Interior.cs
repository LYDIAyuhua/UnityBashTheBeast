using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interior : MonoBehaviour {
	
	public int interiorID;
	public Sprite[] interiors;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().sprite = interiors[interiorID];
	}
}
