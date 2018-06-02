using UnityEngine;
using System.Collections;

public partial class BaseUnit {
	private float _timeBetweenAttacks;
	private float _attackTimer;

	protected IEnumerator Fighting_EnterState() {
		_attackTimer = _timeBetweenAttacks;
		_animator.SetBool("IsFighting", true);
		_animator.speed = AttackSpeed;
		yield return null;
	}
	protected IEnumerator Fighting_ExitState() {
		_animator.SetBool("IsFighting", false);
		_animator.speed = 1.0f;
		yield return null;
	}

	protected virtual void Fighting_Update() {
		if (Target == null || !Target.CanBeAttacked()) {
			OnFightingEnded();
			return;
		}

		Quaternion targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * TurnSpeed);

		_attackTimer = Mathf.Max(0.0f, _attackTimer - Time.deltaTime);
		if (_attackTimer <= 0.0f) {
			_attackTimer = _timeBetweenAttacks;
		}
	}

	public void OnTargetHit() {
		if (Target != null) {
			CombatManager.Instance.ApplyDamage(Target, Attack, AttackDamage);
			//Target.ApplyDamage(AttackDamage);
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
