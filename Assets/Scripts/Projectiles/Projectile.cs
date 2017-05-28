using UnityEngine;

public class Projectile : MonoBehaviour {
	protected int _damage;
	protected Monster _target;
	public virtual void Inject(int damage, Monster monster) {
		_damage = damage;
		_target = monster;
	}

	protected virtual void OnTargetImpact(int damage, Monster target) {
	}
}
