using UnityEngine;
using Interface.towerShop;
using Interface.towers;
using Interface.wallet;
using Ingame.towers;

public class UIManager : MonoBehaviour {
	private TowerShop _towerShop;
	private TowerUI _towerUI;

	private Interface.units.UnitUI _unitUI;

	private WalletUI _walletUI;

	void Awake() {
		_towerShop = FindObjectOfType<TowerShop>();
		_towerUI = FindObjectOfType<TowerUI>();

		_unitUI = FindObjectOfType<Interface.units.UnitUI>();

		_walletUI = FindObjectOfType<WalletUI>();
	}

	public void Inject(Wallet wallet) {
		_walletUI.Inject(wallet);
	}

	public void ShowTower(TowerFactory towerFactory, Tower tower) {
		_towerShop.ShowUpgrades(towerFactory, tower);
		_towerUI.Show(tower);
	}

	public void ShowUnit(BaseUnit unit) {
		_unitUI.Show(unit);
	}

	public void Refresh() {
		_walletUI.Refresh();
	}
}
