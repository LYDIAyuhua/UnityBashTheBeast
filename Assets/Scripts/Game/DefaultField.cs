using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultField : Field {

	#region implemented abstract members of Field

	[SerializeField]
	int id;
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

	[SerializeField]
	int goAheadNumSteps;
	public override int GoAheadNumSteps 
	{
		get 
		{
			return goAheadNumSteps;
		}
		set 
		{
			goAheadNumSteps = value;
		}
	}

	[SerializeField]
	int goBackNumSteps;
	public override int GoBackNumSteps 
	{
		get 
		{
			return goBackNumSteps;
		}
		set 
		{
			goBackNumSteps = value;
		}
	}

	[SerializeField]
	int gotoField;
	public override int GotoField 
	{
		get 
		{
			return gotoField;
		}
		set 
		{
			gotoField = value;
		}
	}

	[SerializeField]
	ActionType actionType;
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

	[SerializeField]
	FieldType fieldType;
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
