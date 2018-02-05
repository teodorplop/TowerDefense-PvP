using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : Projectile {
	[SerializeField]
	private GameObject deathObject;
	[SerializeField]
	private float explosionRange;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Map")) {
			List<Monster> monsters = _owner.GetMonstersInRange(transform.position, explosionRange);
			foreach (Monster monster in monsters)
				monster.ApplyDamage(_damage);

			Die();
		}
	}

	void Die() {
		Instantiate(deathObject, transform.position, Quaternion.identity);
	}
}
