using System;
using System.Collections.Generic;
using Lua;

[Serializable]
public class MonsterBase {
	public string name;
	public readonly Dictionary<string, float> baseStats;
	public LuaWrapper actions;
}
