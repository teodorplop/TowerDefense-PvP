using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherProjectile : Projectile {
	protected override void OnTargetReached(int damage, Monster target) {
		base.OnTargetReached(damage, target);

		target.ApplyDamage(damage);
	}
}
