using UnityEngine;

namespace Ingame.towers {
	public partial class BarracksTower : Tower {
		public enum TowerState { Construction, Active }

		private string _trainedUnit { get { return _attributes.trainedUnit; } }
		private int _maxUnits { get { return _attributes.maxUnits; } }
		private float _range { get { return _attributes.range; } }

		protected new void Awake() {
			base.Awake();
			
			SetState(TowerState.Construction);
		}

		public void Inject(UnitFactory unitFactory) {
			_unitFactory = unitFactory;
		}
	}
}
