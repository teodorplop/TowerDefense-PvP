using UnityEngine;
using Interface.towershop;
using Interface.wallet;
using Ingame.towers;

public class UIManager : MonoBehaviour {
	private TowerShop _towerShop;
	private WalletUI _walletUI;

	void Awake() {
		_towerShop = FindObjectOfType<TowerShop>();
		_walletUI = FindObjectOfType<WalletUI>();
	}

	public void Inject(Wallet wallet) {
		_walletUI.Inject(wallet);
	}

	public void ShowUpgrades(TowerFactory towerFactory, Tower tower) {
		_towerShop.ShowUpgrades(towerFactory, tower);
	}

	public void Refresh() {
		_walletUI.Refresh();
	}
}
