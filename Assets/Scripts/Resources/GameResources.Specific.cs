using System.IO;
using Utils.IO;
using Lua;

public static partial class GameResources {
	private static string _towersRoot = Path.Combine(resourcesRoot, "Towers");
	private static string _monstersRoot = Path.Combine(resourcesRoot, "Monsters");

	/// <summary>
	/// Load all game specific resources into memory
	/// </summary>
	public static void LoadAll() {
		LuaResources.LoadAll();
		LoadTowers();
	}

	/// <summary>
	/// Unloads all resources, including game specific ones
	/// </summary>
	public static void GSUnloadAll() {
		LuaResources.UnloadAll();
		UnloadAll();
	}

	private static void LoadTowers() {
		string[] files = DirectoryIO.GetFileNames(_towersRoot, ".json");
		foreach (string file in files) {
			TowerBase towerBase = Load<TowerBase>(file);
			towerBase.name = Path.GetFileNameWithoutExtension(file);
			towerBase.actions = LuaResources.Load(Path.Combine(_towersRoot, towerBase.name + "_Actions"));
		}
	}

	private static void LoadMonsters() {
		string[] files = DirectoryIO.GetFileNames(_monstersRoot, ".json");
		foreach (string file in files) {
			MonsterBase monsterBase = Load<MonsterBase>(file);
			monsterBase.name = Path.GetFileNameWithoutExtension(file);
			monsterBase.actions = LuaResources.Load(Path.Combine(_monstersRoot, monsterBase.name + "_Actions"));
		}
	}
}
