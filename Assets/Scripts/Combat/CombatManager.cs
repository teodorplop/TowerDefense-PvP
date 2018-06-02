using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Normal, Piercing, Siege, Magic }
public enum ArmorType { Light, Medium, Heavy }

public class Combat_Data : CSVLoader {
	private int attackTypesCount, armorTypesCount;
	private float[][] attackValues;
	
	public float GetAttackValue(AttackType attack, ArmorType armor) {
		return attackValues[(int)attack][(int)armor];
	}

	public Combat_Data(string text) : base(text) {
	}

	protected override void Process() {
		attackTypesCount = EnumExtensions.GetLength<AttackType>();
		armorTypesCount = EnumExtensions.GetLength<ArmorType>();

		attackValues = new float[attackTypesCount][];
		for (int i = 0; i < attackTypesCount; ++i) {
			attackValues[i] = new float[armorTypesCount];
			for (int j = 0; j < armorTypesCount; ++j)
				attackValues[i][j] = 1.0f;
		}

		base.Process();
	}

	protected override void ProcessLine(int line, string[] values) {
		if (values.Length != 3) {
			DebugError(line, "Should contain three values: AttackType, ArmorType, value");
			return;
		}

		AttackType attack;
		ArmorType armor;
		float value;

		object obj = Enum.Parse(typeof(AttackType), values[0]);
		if (obj == null) {
			DebugError(line, "Could not parse AttackType");
			return;
		}
		attack = (AttackType)obj;

		obj = Enum.Parse(typeof(ArmorType), values[1]);
		if (obj == null) {
			DebugError(line, "Could not parse ArmorType");
			return;
		}
		armor = (ArmorType)obj;

		if (!float.TryParse(values[2], out value)) {
			DebugError(line, "Could not parse value");
			return;
		}

		attackValues[(int)attack][(int)armor] = value;
	}
}

public class CombatManager : SingletonComponent<CombatManager> {
	private Combat_Data combatData;

	void Start() {
		combatData = GameResources.LoadCombat();
	}

	public void ApplyDamage(BaseUnit target, AttackType attackType, int damage) {
		float value = combatData.GetAttackValue(attackType, target.Armor);
		damage = Mathf.RoundToInt(value * damage);
		target.ApplyDamage(damage);
	}
}
