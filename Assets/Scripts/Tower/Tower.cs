﻿using MoonSharp.Interpreter;

[MoonSharpUserData]
public class Tower {
	private readonly TowerBase _towerBase;
	public TowerBase towerBase { get { return _towerBase; } }

	private readonly Stats _stats;
	public Stats stats { get { return _stats; } }

	public Tower(TowerBase towerBase) {
		_towerBase = towerBase;
		_stats = new Stats(_towerBase.baseStats);
	}

	private void OnSpawn() {
		towerBase.actions.Call("OnSpawn", this);
	}
	private void OnAttack() {
		towerBase.actions.Call("OnAttack", this);
	}
	private void OnSell() {
		towerBase.actions.Call("OnSell", this);
	}
	private void OnDestroy() {
		towerBase.actions.Call("OnDestroy", this);
	}
}
