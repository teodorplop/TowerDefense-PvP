using UnityEngine;

namespace Ingame.towers {
	public partial class BarracksTower : Tower {
		public enum TowerState { Construction, Active, Destroyed }

		private string _trainedUnit { get { return _attributes.trainedUnit; } }
		private int _maxUnits { get { return _attributes.maxUnits; } }
		public float RespawnTimer { get { return _attributes.respawnTimer; } }
		private float _range { get { return _attributes.range; } }

		[SerializeField]
		private GameObject _rangeSprite;

		protected override void Awake() {
			base.Awake();
			
			SetState(TowerState.Construction);
		}
		void Start() {
			Vector3 scale = _rangeSprite.transform.localScale;
			scale.x *= _range;
			scale.y *= _range;
			_rangeSprite.transform.localScale = scale;
		}

		public void Inject(UnitFactory unitFactory) {
			_unitFactory = unitFactory;
		}
		public override void Select(bool selected) {
			_rangeSprite.SetActive(selected);
		}
		public void ResetRallyPoint(Vector3 point) {
			if (!IsValidRallyPoint(point)) {
				Debug.Log("Rally point not valid.");
				return;
			}

			SetRallyPoint(point);
			for (int i = 0; i < _maxUnits; ++i) {
				_units[i].SetRallyPoint(_rallyPoints[i]);
			}
		}
	}
}
