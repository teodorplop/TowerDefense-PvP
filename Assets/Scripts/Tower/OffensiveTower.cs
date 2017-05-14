using UnityEngine;

public class OffensiveTower : Tower {
	[SerializeField, Tooltip("Attacks per second")]
	private float _attackSpeed;
	[SerializeField]
	private float _attackDamage;

	private float _attackTimer;
	private float _timeBetweenAttacks;

	void Awake() {
		_attackTimer = _timeBetweenAttacks = 1.0f / _attackSpeed;
	}

	protected virtual void OnAttack() {

	}

	void FixedUpdate() {
		_attackTimer = Mathf.Max(_attackTimer - Time.deltaTime, 0.0f);
		if (_attackTimer <= 0.0f) {
			OnAttack();
		}
	}
}
