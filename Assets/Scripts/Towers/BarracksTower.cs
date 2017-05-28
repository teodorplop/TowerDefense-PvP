using System.Collections.Generic;
using UnityEngine;

namespace Ingame.towers {
	public partial class BarracksTower : Tower {
		public enum TowerState { Construction, Active }

		[SerializeField]
		protected Unit _unitPrefab;

		private int _maxUnits { get { return _attributes.maxUnits; } }
		private float _range { get { return _attributes.range; } }

		protected new void Awake() {
			base.Awake();

			_activeUnits = new List<Unit>();
			_rallyPoints = new List<Vector3>();
			SetState(TowerState.Construction);
		}
	}
}
