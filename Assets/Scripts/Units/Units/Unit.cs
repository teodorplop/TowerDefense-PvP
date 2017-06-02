using UnityEngine;

public partial class Unit : BaseUnit {
	public enum UnitState { RallyPoint, ReturnToRallyPoint }

	private float EngageRange { get { return _attributes.engageRange; } }

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
