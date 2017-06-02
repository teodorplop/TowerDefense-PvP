using System.Collections;
using UnityEngine;

namespace Ingame.towers {
	public partial class BarracksTower {
		private UnitFactory _unitFactory;
		private Unit[] _units;
		public Unit[] Units { get { return _units; } }
		private Vector3 _spawnPoint;
		private Vector3 _rallyPoint;
		private Vector3[] _rallyPoints;

		private float[] _respawnTimers;

		protected IEnumerator Active_EnterState() {
			_rallyPoints = new Vector3[_maxUnits];
			_units = new Unit[_maxUnits];
			_respawnTimers = new float[_maxUnits];
			for (int i = 0; i < _maxUnits; ++i) {
				_respawnTimers[i] = RespawnTimer;
			}

			Vector3 bestRallyPoint = PathRequestManager.GetConvenientPoint(owner, transform.position - owner.WorldOffset, _range) + owner.WorldOffset;
			SetRallyPoint(bestRallyPoint);

			yield return StartCoroutine(SpawnUnits());

			yield return null;
		}

		private bool IsValidRallyPoint(Vector3 point) {
			float distance = Vector3.Distance(point, transform.position);
			return distance <= _range;
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
				SpawnUnit(i);

				yield return new WaitForSeconds(0.5f);
			}
		}

		protected void Active_FixedUpdate() {
			for (int i = 0; i < _maxUnits; ++i) {
				if (_units[i].IsDead) {
					_respawnTimers[i] = Mathf.Max(0.0f, _respawnTimers[i] - Time.fixedDeltaTime);

					if (_respawnTimers[i] <= 0.0f) {
						_units[i].Respawn();
						SpawnUnit(i);
						_respawnTimers[i] = RespawnTimer;
					}
				}
			}
		}

		private void SpawnUnit(int idx) {
			_units[idx].transform.position = _spawnPoint;
			_units[idx].gameObject.SetActive(true);
			_units[idx].SetRallyPoint(_rallyPoints[idx]);
		}

		protected void Active_OnDrawGizmos() {
			if (_debug) {
				Gizmos.DrawWireSphere(transform.position, _range);
			}
		}
	}
}
