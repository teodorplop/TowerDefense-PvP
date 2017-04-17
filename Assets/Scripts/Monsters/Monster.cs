using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : ITickable {
	private readonly MonsterBase _monsterBase;
	public MonsterBase monsterBase { get { return _monsterBase; } }

	private readonly Stats _stats;
	public Stats stats { get { return _stats; } }

	public Monster(MonsterBase monsterBase) {
		_monsterBase = monsterBase;
		_stats = new Stats(_monsterBase.baseStats);
	}

	private void OnSpawn() {
		TickEngine.Register(this);
	}
	private void OnDestroy() {
		TickEngine.Unregister(this);
	}

	public void Tick() {
	}
}
