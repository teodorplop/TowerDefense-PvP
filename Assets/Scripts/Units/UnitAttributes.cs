﻿using System;
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
}