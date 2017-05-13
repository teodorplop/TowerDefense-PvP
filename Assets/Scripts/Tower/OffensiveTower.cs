using UnityEngine;

public class OffensiveTower : Tower {
	[SerializeField, Tooltip("Attacks per second")]
	private float _attackSpeed;
	[SerializeField]
	private float _attackDamage;

	private float _timeBetweenAttacks;

	void Awake() {
		_timeBetweenAttacks = 1.0f / _attackSpeed;
	}

	protected virtual void OnAttack() {

	}

	protected override void FixedUpdate() {
		base.FixedUpdate();

		if (_timer >= _timeBetweenAttacks) {
			_timer -= _timeBetweenAttacks;
			OnAttack();
		}
	}
}
