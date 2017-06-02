using System;
using UnityEngine;

[Serializable]
public class UnitAttributes {
	[SerializeField]
	public float movementSpeed;
	[SerializeField]
	public float turnSpeed;

	[SerializeField]
	public int maxHealth;

	[SerializeField]
	public float engageRange;
	[SerializeField]
	public float attackSpeed;
	[SerializeField]
	public float attackRange;
	[SerializeField]
	public int attackDamage;

	[SerializeField]
	public int awardedGold;

	public UnitAttributes Clone() {
		UnitAttributes clone = new UnitAttributes();
		clone.movementSpeed = movementSpeed;
		clone.turnSpeed = turnSpeed;

		clone.maxHealth = maxHealth;

		clone.engageRange = engageRange;
		clone.attackSpeed = attackSpeed;
		clone.attackRange = attackRange;
		clone.attackDamage = attackDamage;

		clone.awardedGold = awardedGold;

		return clone;
	}
}
