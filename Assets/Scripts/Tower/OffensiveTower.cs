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
	private List<Monster> _inRange;

	void Awake() {
		_attackTimer = _timeBetweenAttacks = 1.0f / _attackSpeed;
		_inRange = new List<Monster>();
	}

	protected virtual void OnAttack(Monster target) {

	}

	void FixedUpdate() {
		_attackTimer = Mathf.Max(_attackTimer - Time.deltaTime, 0.0f);
		if (_attackTimer <= 0.0f) {
			//OnAttack();
		}
	}
}
