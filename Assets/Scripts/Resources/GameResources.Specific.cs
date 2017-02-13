using System.IO;
using Utils.IO;
using Lua;

public static partial class GameResources {
	private static string _towersRoot = Path.Combine(_resourcesRoot, "Towers");

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

	/// <summary>
	/// Loads all towers into memory
	/// </summary>
	private static void LoadTowers() {
		string[] directories = DirectoryIO.GetDirectories(_towersRoot);
		foreach (string directory in directories) {
			TowerBase towerBase = Load<TowerBase>(Path.Combine(directory, "TowerBase"));
			towerBase.name = directory.Remove(0, _towersRoot.Length + 1);
			towerBase.actions = LuaResources.Load(Path.Combine(directory.Remove(0, _resourcesRoot.Length + 1), "Actions"));
		}
	}
}
