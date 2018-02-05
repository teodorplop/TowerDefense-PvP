using UnityEngine;

public partial class Unit : BaseUnit {
	public enum UnitState { RallyPoint, ReturnToRallyPoint }
	
	private float _idleTime = 0.0f;

	private float EngageRange { get { return _attributes.engageRange; } }

	private void ResetIdleTime() {
		_idleTime = 0;
	}
	private void UpdateIdleTime(float time) {
		_idleTime += time;
		if (_idleTime >= _attributes.regenTime)
			_currentHealth = MaxHealth;
	}

	public void SetRallyPoint(Vector3 point) {
		RemoveTarget();

		_rallyPoint = point;
		_refreshRallyPoint = true;
		SetState(UnitState.RallyPoint);
	}

	public void Respawn() {
		_animator.SetBool("IsDead", false);
		_currentHealth = MaxHealth;
	}

	public override bool CanBeAttacked() {
		return !IsDead;
	}
}
