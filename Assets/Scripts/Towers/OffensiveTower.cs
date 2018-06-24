using UnityEngine;

namespace Ingame.towers {
	public partial class OffensiveTower : Tower {
		[SerializeField]
		private GameObject _rangeSprite;

		public float AttackSpeed { get { return _attributes.attackSpeed; } }
		public float Range { get { return _attributes.range; } }
		public int AttackDamage { get { return _attributes.attackDamage; } }
		public AttackType Attack { get { return _attributes.attack; } }

		protected override void Awake() {
			base.Awake();
			SetState(TowerState.Construction);
		}
		protected virtual void Start() {
			Vector3 scale = _rangeSprite.transform.localScale;
			scale.x *= Range;
			scale.y *= Range;
			_rangeSprite.transform.localScale = scale;
		}

		public override void Select(bool selected) {
			_rangeSprite.SetActive(selected);
		}
	}
}
