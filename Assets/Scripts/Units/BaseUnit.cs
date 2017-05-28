using UnityEngine;
using System;
using Pathfinding;

public partial class BaseUnit : StateMachineBase {
	public enum BaseUnitState { Idle, Walking, Destination, Fighting, Dead }

	[SerializeField]
	protected int _maxHealth = 100;
	[SerializeField]
	protected bool _debug;

	public Player owner;

	public int MaxHealth { get { return _maxHealth; } }
	public bool DebugOn { get { return _debug; } }
	protected int _currentHealth;
	public int CurrentHealth { get { return _currentHealth; } }
	protected bool _isDead;
	public bool IsDead { get { return _isDead; } }

	protected new void Awake() {
		base.Awake();
		_currentHealth = _maxHealth;
	}

	protected void SetState(Enum state) {
		if (currentState == null || currentState.GetType() != state.GetType() || currentState.CompareTo(state) != 0) {
			_stateMachineHandler.SetState(state, this);
		}
	}

	protected int _waypointIndex;
	protected Path _waypointsPath;
	[SerializeField]
	private float _movementSpeed = 5.0f;
	[SerializeField]
	private float _turnSpeed = 5.0f;

	protected void FollowWaypoints(Action<bool> ifFinished) {
		if (_waypointsPath == null) {
			return;
		}

		if (_waypointIndex == _waypointsPath.turnBoundaries.Length) {
			// We passed through all our waypoints
			ifFinished(false);
			return;
		}

		Vector3 position = transform.position - owner.WorldOffset;
		Vector2 position2D = new Vector2(position.x, position.z);

		if (_waypointsPath.turnBoundaries[_waypointIndex].HasCrossedLine(position2D)) {
			++_waypointIndex;

			if (_waypointIndex == _waypointsPath.turnBoundaries.Length) {
				ifFinished(true);
				return;
			}
		}

		Quaternion targetRotation = Quaternion.LookRotation(_waypointsPath.waypoints[_waypointIndex] - position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * _turnSpeed);
		transform.Translate(Vector3.forward * Time.fixedDeltaTime * _movementSpeed);
	}

	// TODO: also pass a damage type parameter, for damage calculation
	public void ApplyDamage(int damage) {
		_currentHealth = Mathf.Max(0, _currentHealth - damage);
		if (_currentHealth == 0) {
			_isDead = true;
			SetState(BaseUnitState.Dead);
		}
	}
}
