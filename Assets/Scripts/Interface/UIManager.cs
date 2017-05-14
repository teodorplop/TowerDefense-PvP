using UnityEngine;
using Interface.towershop;
using Interface.wallet;

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

	public void ShowUpgrades(Tower tower) {
		_towerShop.ShowUpgrades(tower);
	}
}
