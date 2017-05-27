using UnityEngine;

public partial class OffensiveTower : Tower {
	public enum TowerState { Construction, Active }

	[SerializeField, Tooltip("Attacks per second")]
	private float _attackSpeed;
	[SerializeField, Tooltip("Attack radius in world units")]
	private float _radius;
	[SerializeField]
	private float _attackDamage;
	[SerializeField]
	private bool _debug;

	protected new void Awake() {
		base.Awake();
		SetState(TowerState.Construction);
	}
}
