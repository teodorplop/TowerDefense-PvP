using UnityEngine;

namespace Ingame.towers {
	public class ArcherTower : OffensiveTower {
		[SerializeField]
		private Transform _launcher;

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
			if (_target != null) {
				Quaternion targetRotation = Quaternion.LookRotation(_target.transform.position - transform.position);
				_launcher.rotation = Quaternion.Lerp(_launcher.rotation, targetRotation, Time.deltaTime * 15.0f);
			}
		}
	}
}
