using UnityEngine;
using System;
using Pathfinding;
using System.Collections.Generic;

public partial class BaseUnit : StateMachineBase {
	public enum BaseUnitState { Idle, Walking, Engaging, Fighting, Dead }

	protected Animator _animator;
	[SerializeField]
	protected UnitAttributes _attributes;
	[SerializeField]
	protected bool _debug;
	[SerializeField]
	private float _height;

	public Player owner;

	public int MaxHealth { get { return _attributes.maxHealth; } }
	public bool DebugOn { get { return _debug; } }
	protected int _currentHealth;
	public int CurrentHealth { get { return _currentHealth; } }
	public bool IsDead { get { return _currentHealth == 0; } }
	public float AttackRange { get { return _attributes.attackRange; } }
	public int AttackDamage { get { return _attributes.attackDamage; } }
	
	public float AttackSpeed { get { return _attributes.attackSpeed; } }
	public float MovementSpeed { get { return _attributes.movementSpeed; } }
	private float TurnSpeed { get { return _attributes.turnSpeed; } }

	public float Height { get { return _height; } }

	protected override void Awake() {
		base.Awake();

		_animator = GetComponent<Animator>();
		_targets = new List<BaseUnit>();
	}

	protected void SetState(Enum state) {
		if (currentState == null || currentState.GetType() != state.GetType() || currentState.CompareTo(state) != 0) {
			_stateMachineHandler.SetState(state, this);
		}
	}

	public void SetAttributes(UnitAttributes attributes) {
		_attributes = attributes;
		_currentHealth = _attributes.maxHealth;
		_timeBetweenAttacks = _attackTimer = 1.0f / attributes.attackSpeed;
	}

	protected int _waypointIndex;
	protected Path _waypointsPath;

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
		position.y = 0;
		Vector2 position2D = new Vector2(position.x, position.z);

		while (_waypointsPath.turnBoundaries[_waypointIndex].HasCrossedLine(position2D)) {
			++_waypointIndex;

			if (_waypointIndex == _waypointsPath.turnBoundaries.Length) {
				ifFinished(true);
				return;
			}
		}

		Quaternion targetRotation = Quaternion.LookRotation(_waypointsPath.waypoints[_waypointIndex] - position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * TurnSpeed);
		transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);

		float height = owner.Terrain.SampleHeight(transform.position);
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
	}

	// TODO: also pass a damage type parameter, for damage calculation
	public void ApplyDamage(int damage) {
		if (_currentHealth == 0) {
			return;
		}

		_currentHealth = Mathf.Max(0, _currentHealth - damage);
		if (IsDead) {
			SetState(BaseUnitState.Dead);
		}
	}

	public virtual bool CanBeAttacked() {
		return true;
	}
}
