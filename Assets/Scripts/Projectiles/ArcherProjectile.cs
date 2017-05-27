using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherProjectile : Projectile {
	protected override void OnTargetImpact(int damage, Monster target) {
		base.OnTargetImpact(damage, target);

		target.ApplyDamage(damage);
	}
}
