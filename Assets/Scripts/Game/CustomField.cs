using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomField : Field {

	#region implemented abstract members of Field

	public int id;
	public override int ID 
	{
		get 
		{
			return id;
		}
		set 
		{
			id = value;
		}
	}

	#endregion

	#region implemented virtual members of Field

	ActionType actionType = ActionType.Custom;
	public override ActionType Action 
	{
		get 
		{
			return actionType;
		}
		set 
		{
			actionType = value;
		}
	}

	FieldType fieldType = FieldType.Action;
	public override FieldType Type 
	{
		get 
		{
			return fieldType;
		}
		set 
		{
			fieldType = value;
		}
	}

	#endregion

	/// <summary>
	/// override to perform custom Action
	/// </summary>
	/// <param name="player">Player.</param>
	public override void DoAction (Player player)
	{
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
