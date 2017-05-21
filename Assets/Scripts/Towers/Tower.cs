using UnityEngine;
using System;

public class Tower : StateMachineBase {
	[SerializeField]
	private string[] _upgrades;
	public string[] Upgrades { get { return _upgrades; } }

	public Player owner;

	protected void SetState(Enum state) {
		if (currentState == null || currentState != state) {
			_stateMachineHandler.SetState(state, this);
		}
	}
}
