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

	private UIPlayerStats _playerStats;

	private EndGameUI _endGameUI;

	[SerializeField] private GameObject _escapeCanvas;

	void Awake() {
		_towerShop = FindObjectOfType<TowerShop>();

		_towerUI = FindObjectOfType<TowerUI>();
		_unitUI = FindObjectOfType<Interface.units.UnitUI>();

		_walletUI = FindObjectOfType<WalletUI>();
		_sendMonstersShop = FindObjectOfType<SendMonstersShop>();

		_playerStats = FindObjectOfType<UIPlayerStats>();

		_endGameUI = FindObjectOfType<EndGameUI>();
	}

	public void Inject(Wallet wallet, MonsterFactory monsterFactory, SendMonstersList sendMonsters) {
		_walletUI.Inject(wallet);
		_sendMonstersShop.Inject(monsterFactory, sendMonsters);
	}

	public void Inject(Player[] players) {
		_playerStats.InjectPlayers(players);
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
		_playerStats.Refresh();
	}

	public void GameEnded(int mmr, bool win) {
		_endGameUI.Show(mmr, win);
	}

	public void Escape() {
		_escapeCanvas.SetActive(!_escapeCanvas.activeSelf);
	}
}
