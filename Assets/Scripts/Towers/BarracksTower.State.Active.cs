﻿using System.Collections;
using UnityEngine;

namespace Ingame.towers {
	public partial class BarracksTower {
		private float _spawnTimer;

		private UnitFactory _unitFactory;
		private Unit[] _units;
		private Vector3 _spawnPoint;
		private Vector3 _rallyPoint;
		private Vector3[] _rallyPoints;

		protected IEnumerator Active_EnterState() {
			_rallyPoints = new Vector3[_maxUnits];
			_units = new Unit[_maxUnits];

			Vector3 bestRallyPoint = PathRequestManager.GetConvenientPoint(owner, transform.position - owner.WorldOffset, _range) + owner.WorldOffset;
			SetRallyPoint(bestRallyPoint);

			yield return StartCoroutine(SpawnUnits());

			yield return null;
		}

		private void SetRallyPoint(Vector3 point) {
			_rallyPoint = point;
			
			float angle = 0;
			for (int i = 0; i < _maxUnits; ++i) {
				Vector3 offset = 1.75f * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
				angle += Mathf.PI * 2.0f / _maxUnits;

				_rallyPoints[i] = _rallyPoint + offset;
			}

			Vector3 direction = _rallyPoint - transform.position;
			_spawnPoint = transform.position + direction * 0.25f;
		}

		private IEnumerator SpawnUnits() {
			for (int i = 0; i < _maxUnits; ++i) {
				_units[i] = _unitFactory.InstantiateUnit(owner, this, _trainedUnit);
				
				_units[i].transform.position = _spawnPoint;
				_units[i].gameObject.SetActive(true);
				_units[i].SetRallyPoint(owner.WorldOffset + _rallyPoints[i]);

				yield return new WaitForSeconds(0.5f);
			}
		}

		protected void Active_OnDrawGizmos() {
			if (_debug) {
				Gizmos.DrawWireSphere(transform.position, _range);
			}
		}
	}
}
