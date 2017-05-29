using UnityEngine;
using System.Collections.Generic;

public partial class Unit {
	private static float _engagingRange = 5.0f;

	void Idle_FixedUpdate() {
		List<Monster> inRange = owner.GetMonstersInRange(transform.position, _engagingRange);
		float distance = Mathf.Infinity;
		foreach (Monster monster in inRange) {
			if (!monster.CanBeAttacked()) {
				continue;
			}

			float monsterDistance = Vector3.Distance(transform.position, monster.transform.position);
			if (monsterDistance < distance) {
				distance = monsterDistance;
				_target = monster;
			}
		}

		if (_target != null) {
			Vector3 engagePoint = (transform.position + _target.transform.position) / 2;

			_target.Engage(this, engagePoint);
			Engage(_target, engagePoint);
		}
	}

	void Idle_OnDrawGizmos() {
		if (_debug) {
			Gizmos.DrawWireSphere(transform.position, _engagingRange);
		}
	}
}
