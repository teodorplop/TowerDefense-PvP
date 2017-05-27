using UnityEngine;

public class ArcherTower : OffensiveTower {
	protected override void OnAttack(Monster target) {
		base.OnAttack(target);

		Projectile projectile = Instantiate(_projectilePrefab);
		projectile.transform.SetParent(transform);
		projectile.transform.localPosition = _projectilePrefab.transform.localPosition;
		projectile.transform.localScale = _projectilePrefab.transform.localScale;

		projectile.Inject(_attackDamage, target);
		projectile.gameObject.SetActive(true);
	}
}
