using Ingame.towers;
using Ingame.waves;
using System.Collections.Generic;

public partial class GameResources {
	private static readonly string _walletPath = "Wallet";
	private static readonly string _towerAttributesPath = "TowerAttributes";
	private static readonly string _monsterAttributesPath = "MonsterAttributes";
	private static readonly string _unitAttributesPath = "UnitAttributes";
	private static readonly string _wavesPath = "Waves";
	private static readonly string _sendMonstersPath = "SendMonsters";

	public static SendMonstersList LoadSendMonsters(string level) {
		return Load<SendMonstersList>(level, _sendMonstersPath, "SendMonsters");
	}

	public static SendMonstersList LoadSendMonsters() {
		return LoadSendMonsters(SceneLoader.ActiveScene);
	}

	public static Wallet LoadWallet(string level) {
		return Load<Wallet>(level, _walletPath, "Wallet");
	}
	public static Wallet LoadWallet() {
		return LoadWallet(SceneLoader.ActiveScene);
	}

	public static TowerAttributes LoadTowerAttributes(string level, string tower) {
		return Load<TowerAttributes>(level, _towerAttributesPath, tower);
	}
	public static TowerAttributes LoadTowerAttributes(string tower) {
		return LoadTowerAttributes(SceneLoader.ActiveScene, tower);
	}

	public static UnitAttributes LoadMonsterAttributes(string level, string monster) {
		return Load<UnitAttributes>(level, _monsterAttributesPath, monster);
	}
	public static UnitAttributes LoadMonsterAttributes(string monster) {
		return LoadMonsterAttributes(SceneLoader.ActiveScene, monster);
	}

	public static UnitAttributes LoadUnitAttributes(string level, string unit) {
		return Load<UnitAttributes>(level, _unitAttributesPath, unit);
	}
	public static UnitAttributes LoadUnitAttributes(string unit) {
		return LoadUnitAttributes(SceneLoader.ActiveScene, unit);
	}

	public static List<Wave> LoadWaves(string level) {
		return Load<List<Wave>>(_wavesPath + '/' + level);
	}
	public static List<Wave> LoadWaves() {
		return LoadWaves(SceneLoader.ActiveScene);
	}
}
