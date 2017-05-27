using Ingame.towers;

public partial class GameResources {
	private static readonly string _walletPath = "Wallet";
	private static readonly string _towerAttributesPath = "TowerAttributes";

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
}
