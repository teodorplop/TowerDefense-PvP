using System;
using System.Collections.Generic;
using Lua;

[Serializable]
public class TowerBase {
	public string name;
	public readonly Dictionary<string, float> baseStats;
	public LuaWrapper actions;
}
