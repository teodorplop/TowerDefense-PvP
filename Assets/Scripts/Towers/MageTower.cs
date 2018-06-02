using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame.towers {
	public class MageTower : ProjectileTower {
		/*protected override void LaunchProjectile() {
			Projectile projectile = Instantiate(_projectilePrefab);
			projectile.transform.SetParent(_launchPoint);
			projectile.transform.localPosition = Vector3.zero;
			projectile.transform.localRotation = Quaternion.identity;
			projectile.Inject(Attack, AttackDamage, _target, owner);
			projectile.gameObject.SetActive(true);
		}*/
	}
}
