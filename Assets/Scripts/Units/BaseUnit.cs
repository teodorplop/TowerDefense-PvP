using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseUnit : StateMachineBase {
	public enum UnitState { Idle, Walking, Destination, Fighting, Dead }

	[SerializeField]
	private int _maxHealth = 100;
	[SerializeField]
	private bool _debug;

	public Player owner;

	public int MaxHealth { get { return _maxHealth; } }
	public bool DebugOn { get { return _debug; } }
	private int _currentHealth;
	public int CurrentHealth { get { return _currentHealth; } }

	void Start() {
		_currentHealth = _maxHealth;

		SetState(UnitState.Idle);
	}

	// TODO: also pass a damage type parameter, for damage calculation
	public void ApplyDamage(int damage) {
		_currentHealth = Mathf.Max(0, _currentHealth - damage);
		if (_currentHealth == 0) {
			SetState(UnitState.Dead);
		}
	}

	private void SetState(UnitState state) {
		if (currentState == null || (UnitState)currentState != state) {
			_stateMachineHandler.SetState(state, this);
		}
	}
}
