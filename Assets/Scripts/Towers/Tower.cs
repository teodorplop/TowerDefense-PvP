using UnityEngine;
using System;

public class Tower : StateMachineBase {
	protected static readonly float _constructionTime = 1.0f;

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
