using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : FollowProjectile {
	[SerializeField]
	private float explosionRange;

	protected override void OnTargetImpact(int damage, Monster target) {
		base.OnTargetImpact(damage, target);

		List<Monster> monsters = target.owner.GetMonstersInRange(transform.position, explosionRange);
		foreach (Monster monster in monsters)
			monster.ApplyDamage(damage);
	}
}
