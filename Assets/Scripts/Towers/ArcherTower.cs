using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : OffensiveTower {
	protected override void OnAttack(Monster target) {
		base.OnAttack(target);

		Debug.Log(name + " OnAttack " + target.name, gameObject);
	}
}
