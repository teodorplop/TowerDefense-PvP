using Ingame.towers;
using Ingame.waves;
using System.Collections.Generic;
using System.IO;

public partial class GameResources {
	private static readonly string _walletPath = "Wallet";
	private static readonly string _towerAttributesPath = "TowerAttributes";
	private static readonly string _monsterAttributesPath = "MonsterAttributes";
	private static readonly string _unitAttributesPath = "UnitAttributes";
	private static readonly string _wavesPath = "Waves";
	private static readonly string _sendMonstersPath = "SendMonsters";

	private static string GetPath(string root, string level, string file) {
		return Path.Combine(root, string.Format("{0}_{1}", file, level));
	}
	private static string GetDefaultPath(string root, string level, string file) {
		return Path.Combine(root, file);
	}

	private static T LoadJSON<T>(string root, string level, string file) where T : IGameResource {
		string path = GetPath(root, level, file);
		T t = LoadJSON<T>(path);
		if (t == null) {
			path = GetDefaultPath(root, level, file);
			t = LoadJSON<T>(path);
		}
		return t;
	}
	private static T LoadCSV<T>(string root, string level, string file) where T : CSVLoader, IGameResource {
		string path = GetPath(root, level, file);
		T t = LoadCSV<T>(path);
		if (t == null) {
			path = GetDefaultPath(root, level, file);
			t = LoadCSV<T>(path);
		}
		return t;
	}
	private static T LoadFile<T>(string root, string level, string file) where T : ILoadableFile, new() {
		string path = GetPath(root, level, file);
		T t = LoadFile<T>(path);
		if (t == null) {
			path = GetDefaultPath(root, level, file);
			t = LoadFile<T>(path);
		}
		return t;
	}

	public static SendMonstersList LoadSendMonsters(string level) {
		return LoadJSON<SendMonstersList>(_sendMonstersPath, level, "SendMonsters");
	}

	public static SendMonstersList LoadSendMonsters() {
		return LoadSendMonsters(SceneLoader.ActiveScene);
	}

	public static Wallet LoadWallet(string level) {
		return LoadJSON<Wallet>(_walletPath, level, "Wallet");
	}
	public static Wallet LoadWallet() {
		return LoadWallet(SceneLoader.ActiveScene);
	}

	public static TowerAttributes LoadTowerAttributes(string level, string tower) {
		return LoadJSON<TowerAttributes>(_towerAttributesPath, level, tower);
	}
	public static TowerAttributes LoadTowerAttributes(string tower) {
		return LoadTowerAttributes(SceneLoader.ActiveScene, tower);
	}

	public static UnitAttributes LoadMonsterAttributes(string level, string monster) {
		return LoadJSON<UnitAttributes>(_monsterAttributesPath, level, monster);
	}
	public static UnitAttributes LoadMonsterAttributes(string monster) {
		return LoadMonsterAttributes(SceneLoader.ActiveScene, monster);
	}

	public static UnitAttributes LoadUnitAttributes(string level, string unit) {
		return LoadJSON<UnitAttributes>(_unitAttributesPath, level, unit);
	}
	public static UnitAttributes LoadUnitAttributes(string unit) {
		return LoadUnitAttributes(SceneLoader.ActiveScene, unit);
	}

	public static Waves_Data LoadWaves(string level) {
		return LoadFile<Waves_Data>(_wavesPath, level, "Waves");
	}
	public static Waves_Data LoadWaves() {
		return LoadWaves(SceneLoader.ActiveScene);
	}
}
