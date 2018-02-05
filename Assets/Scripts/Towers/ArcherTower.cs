using UnityEngine;

namespace Ingame.towers {
	public class ArcherTower : ProjectileTower {
		protected override void RotateTowards(Transform target) {
			Quaternion targetRotation = _launcherRotation;

			if (target != null) {
				Vector3 p1 = target.position;
				p1.y = 0;
				Vector3 p2 = transform.position;
				p2.y = 0;

				targetRotation = Quaternion.LookRotation(p1 - p2) * Quaternion.Euler(0, 90, 0);
			}

			_launcher.rotation = Quaternion.Lerp(_launcher.rotation, targetRotation, Time.deltaTime * 15.0f);
		}

		protected override void LaunchProjectile() {
			Projectile projectile = Instantiate(_projectilePrefab);
			projectile.transform.SetParent(_launchPoint);
			projectile.transform.localPosition = Vector3.zero;
			projectile.transform.localRotation = Quaternion.identity;
			projectile.Inject(AttackDamage, _target, owner);
			projectile.gameObject.SetActive(true);
		}
	}
}
