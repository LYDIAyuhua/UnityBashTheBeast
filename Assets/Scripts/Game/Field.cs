using UnityEngine;
using System.Collections;

public abstract class Field : MonoBehaviour {
		
		public abstract int ID { get; set; }

		public virtual int GoBackNumSteps { get; set; }
		public virtual int GoAheadNumSteps { get; set; }	
		public virtual int GotoField { get; set; }
		public virtual ActionType Action { get; set; }
		public virtual FieldType Type { get; set; }

		/// <summary>
		/// override to perform custom Action
		/// </summary>
		/// <param name="player">Player.</param>
		public virtual void DoAction(Player player)
		{
		}
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}
}
