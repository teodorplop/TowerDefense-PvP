using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.towers {
	public partial class OffensiveTower {
		private float _attackTimer;
		private float _timeBetweenAttacks;
		protected Monster _target;

		protected IEnumerator Active_EnterState() {
			_attackTimer = _timeBetweenAttacks = 1.0f / AttackSpeed;
			yield return null;
		}

		protected virtual void OnAttack(Monster target) {
			_attackTimer = _timeBetweenAttacks;
		}
		private bool TargetIsStillValid() {
			return _target != null && !_target.IsDead && !_target.ReachedDestination && Vector3Utils.PlanarDistance(_target.transform.position, transform.position) <= Range;
		}
		protected virtual void UpdateTarget() {
			if (!TargetIsStillValid()) {
				_target = null;

				List<Monster> monsters = owner.GetMonstersInRange(transform.position, Range);
				if (monsters.Count > 0) {
					float distance = Mathf.Infinity;
					foreach (Monster monster in monsters) {
						if (!monster.CanBeAttacked()) {
							continue;
						}

						float monsterDistance = Vector3Utils.PlanarDistance(transform.position, monster.transform.position);
						if (monsterDistance < distance) {
							distance = monsterDistance;
							_target = monster;
						}
					}
				}
			}
		}

		protected void Active_FixedUpdate() {
			_attackTimer = Mathf.Max(_attackTimer - Time.fixedDeltaTime, 0.0f);
			if (_attackTimer <= 0.0f) {
				UpdateTarget();
				if (_target) {
					OnAttack(_target);
				}
			}
		}

		protected void Active_OnDrawGizmos() {
			if (_debug) {
				Gizmos.DrawWireSphere(transform.position, Range);
			}
		}
	}
}
