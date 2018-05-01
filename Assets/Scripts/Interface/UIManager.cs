using UnityEngine;
using Interface.towerShop;
using Interface.towers;
using Interface.wallet;
using Interface.sendMonsters;
using Ingame.towers;

public class UIManager : MonoBehaviour {
	private TowerShop _towerShop;

	private TowerUI _towerUI;
	private Interface.units.UnitUI _unitUI;

	private WalletUI _walletUI;
	private SendMonstersShop _sendMonstersShop;

	private EndGameUI _endGameUI;

	void Awake() {
		_towerShop = FindObjectOfType<TowerShop>();

		_towerUI = FindObjectOfType<TowerUI>();
		_unitUI = FindObjectOfType<Interface.units.UnitUI>();

		_walletUI = FindObjectOfType<WalletUI>();
		_sendMonstersShop = FindObjectOfType<SendMonstersShop>();

		_endGameUI = FindObjectOfType<EndGameUI>();
	}

	public void Inject(Wallet wallet, MonsterFactory monsterFactory, SendMonstersList sendMonsters) {
		_walletUI.Inject(wallet);
		_sendMonstersShop.Inject(monsterFactory, sendMonsters);
	}

	public void ShowTower(TowerFactory towerFactory, Tower tower) {
		_towerShop.ShowUpgrades(towerFactory, tower);
		_towerUI.Show(tower);
	}

	public void ShowUnit(BaseUnit unit) {
		_unitUI.Show(unit);
	}

	public void ShowSendMonsters(bool shown) {
		_sendMonstersShop.Show(shown);
	}

	public void Refresh() {
		_walletUI.Refresh();
	}

	public void GameEnded(bool win) {
		_endGameUI.Show(win);
	}
}
