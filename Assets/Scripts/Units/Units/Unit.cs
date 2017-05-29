using UnityEngine;

public partial class Unit : BaseUnit {
	public enum UnitState { RallyPoint }

	private float EngageRange { get { return _attributes.engageRange; } }

	public void SetRallyPoint(Vector3 point) {
		_rallyPoint = point;
		SetState(UnitState.RallyPoint);
	}

	public void Respawn() {
		_currentHealth = MaxHealth;
	}

	public override bool CanBeAttacked() {
		return !IsDead;
	}
}
