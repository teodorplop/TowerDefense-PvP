using UnityEngine;

namespace Ingame.towers {
	public class ArcherTower : OffensiveTower {
		[SerializeField]
		private Transform _launcher;
		private Quaternion _launcherRotation;

		protected new void Start() {
			base.Start();
			_launcherRotation = _launcher.rotation;
		}

		protected override void OnAttack(Monster target) {
			base.OnAttack(target);

			Projectile projectile = Instantiate(_projectilePrefab);
			projectile.transform.SetParent(transform);
			projectile.transform.localPosition = _projectilePrefab.transform.localPosition;
			projectile.transform.localScale = _projectilePrefab.transform.localScale;

			projectile.Inject(AttackDamage, target);
			projectile.gameObject.SetActive(true);
		}

		protected void Active_Update() {
			Quaternion targetRotation = _launcherRotation;
			if (_target != null) {
				Vector3 p1 = _target.transform.position;
				p1.y = 0;
				Vector3 p2 = transform.position;
				p2.y = 0;

				targetRotation = Quaternion.LookRotation(p1 - p2);
				targetRotation *= Quaternion.Euler(0, 90, 0);
			}
			
			_launcher.rotation = Quaternion.Lerp(_launcher.rotation, targetRotation, Time.deltaTime * 15.0f);
		}
	}
}
