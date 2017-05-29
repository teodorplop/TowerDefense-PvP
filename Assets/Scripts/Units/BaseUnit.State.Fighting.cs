using UnityEngine;

public partial class BaseUnit {
	private float _timeBetweenAttacks;
	private float _attackTimer;

	protected virtual void Fighting_FixedUpdate() {
		if (Target == null || !Target.CanBeAttacked()) {
			OnFightingEnded();
			return;
		}

		_attackTimer = Mathf.Max(0.0f, _attackTimer - Time.fixedDeltaTime);
		if (_attackTimer <= 0.0f) {
			_attackTimer = _timeBetweenAttacks;
			Target.ApplyDamage(AttackDamage);
		}
	}

	protected virtual void OnFightingEnded() {
		RemoveTarget();
	}

	protected void Fighting_OnDrawGizmos() {
		if (_debug) {
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(transform.position, AttackRange);
		}
	}
}
