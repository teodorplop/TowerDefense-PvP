using UnityEngine;
using System.Collections.Generic;

public class OffensiveTower : Tower {
	[SerializeField, Tooltip("Attacks per second")]
	private float _attackSpeed;
	[SerializeField, Tooltip("Attack radius in world units")]
	private float _radius;
	[SerializeField]
	private float _attackDamage;

	private float _attackTimer;
	private float _timeBetweenAttacks;
	private Monster _target;

	void Awake() {
		_attackTimer = _timeBetweenAttacks = 1.0f / _attackSpeed;
	}

	protected virtual void OnAttack(Monster target) {
		_attackTimer = _timeBetweenAttacks;
	}

	protected virtual void UpdateTarget() {
		if (_target == null || Vector3.Distance(_target.transform.position, transform.position) > _radius) {
			_target = null;

			List<Monster> monsters = owner.GetMonstersInRange(transform.position, _radius);
			if (monsters.Count > 0) {
				float distance = Mathf.Infinity;
				foreach (Monster monster in monsters) {
					float monsterDistance = Vector3.Distance(transform.position, monster.transform.position);
					if (monsterDistance < distance) {
						distance = monsterDistance;
						_target = monster;
					}
				}
			}
		}
	}

	void FixedUpdate() {
		_attackTimer = Mathf.Max(_attackTimer - Time.deltaTime, 0.0f);
		if (_attackTimer <= 0.0f) {
			UpdateTarget();
			if (_target) {
				OnAttack(_target);
			}
		}
	}
}
