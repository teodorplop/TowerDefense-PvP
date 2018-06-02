using UnityEngine;

public class Projectile : MonoBehaviour {
	protected int _damage;
	protected AttackType _attackType;
	protected Monster _target;
	protected Player _owner;

	public virtual void Inject(AttackType attack, int damage, Monster monster, Player owner) {
		_damage = damage;
		_target = monster;
		_owner = owner;
	}

	protected virtual void OnTargetImpact(int damage, Monster target) {
		CombatManager.Instance.ApplyDamage(target, _attackType, damage);
	}
}
