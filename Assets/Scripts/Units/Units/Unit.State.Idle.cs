using UnityEngine;
using System.Collections.Generic;

public partial class Unit {
	private Monster GetMonsterInRange() {
		List<Monster> inRange = owner.GetMonstersInRange(transform.position, EngageRange);
		float distance = Mathf.Infinity;
		Monster target = null;
		foreach (Monster monster in inRange) {
			if (!monster.CanBeAttacked()) {
				continue;
			}

			float monsterDistance = Vector3Utils.PlanarDistance(transform.position, monster.transform.position);

			if (monster.Target == this) {
				// If the monster is currently targetting us, keep it as a target!
				target = monster;
				distance = monsterDistance;
				break;
			}

			if (target == null) {
				// If we do not have a target yet, this is the best one!
				target = monster;
				distance = monsterDistance;
				continue;
			}

			if (target.Target != null && target.Target != this && monster.Target == null) {
				// Prioritize monster that currently don't have a valid target
				target = monster;
				distance = monsterDistance;
			} else if (target.Target == null && monster.Target != null && monster.Target != this) {
				continue;
			}
			
			if (monsterDistance < distance) {
				distance = monsterDistance;
				target = monster;
			}
		}

		return target;
	}

	void Idle_FixedUpdate() {
		Monster target = GetMonsterInRange();

		if (target != null) {
			Engage(target);
			target.Engage(this);
		}
	}

	void Idle_OnDrawGizmos() {
		if (_debug) {
			Gizmos.DrawWireSphere(transform.position, EngageRange);
		}
	}
}
