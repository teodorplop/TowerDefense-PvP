using System;
using UnityEngine;

[Serializable]
public class UnitAttributes : IGameResource {
	public string Name { get; set; }

	[SerializeField]
	public float movementSpeed;
	[SerializeField]
	public float turnSpeed;

	[SerializeField]
	public int maxHealth;
	[SerializeField]
	public float regenTime;

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
	[SerializeField]
	public int lifeTaken;

	[SerializeField]
	public ArmorType armor;

	[SerializeField]
	public AttackType attack;

	[SerializeField]
	public string uiSprite;
}
