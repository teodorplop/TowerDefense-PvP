using UnityEngine;

namespace Ingame.towers {
	public partial class OffensiveTower : Tower {
		public enum TowerState { Construction, Active }

		[SerializeField]
		protected Projectile _projectilePrefab;

		private float _attackSpeed { get { return _attributes.attackSpeed; } }
		private float _range { get { return _attributes.range; } }
		protected int _attackDamage { get { return _attributes.attackDamage; } }

		protected new void Awake() {
			base.Awake();
			SetState(TowerState.Construction);
		}
	}
}
