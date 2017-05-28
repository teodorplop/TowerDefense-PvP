using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.towers {
	public partial class BarracksTower {
		private float _spawnTimer;
		private Vector3 _rallyPoint;
		
		protected List<Unit> _activeUnits;
		protected List<Vector3> _rallyPoints;

		protected IEnumerator Active_EnterState() {
			DecideRallyPoints();

			SpawnUnits();

			yield return null;
		}

		private void DecideRallyPoints() {
			_rallyPoint = PathRequestManager.GetConvenientPoint(owner, transform.position - owner.WorldOffset, _range);

			float angle = 0;
			for (int i = 0; i < _maxUnits; ++i) {
				Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
				angle += Mathf.PI * 2.0f / _maxUnits;

				_rallyPoints.Add(_rallyPoint + owner.WorldOffset + offset);
			}
		}

		private void SpawnUnits() {
			for (int i = 0; i < _maxUnits; ++i) {
				Unit unit = InstantiateUnit();
				unit.transform.position = owner.WorldOffset + _rallyPoints[i];
				_activeUnits.Add(unit);
			}
		}

		private Unit InstantiateUnit() {
			Unit unit = Instantiate(_unitPrefab);
			unit.transform.SetParent(transform);
			unit.transform.localPosition = _unitPrefab.transform.localPosition;
			unit.transform.localScale = _unitPrefab.transform.localScale;
			unit.gameObject.SetActive(true);

			return unit;
		}

		protected void Active_OnDrawGizmos() {
			if (_debug) {
				Gizmos.DrawWireSphere(transform.position, _range);
			}
		}
	}
}
