using UnityEngine;
using Interface.towershop;

public class UIManager : MonoBehaviour {
	private TowerShop _towerShop;

	void Awake() {
		_towerShop = FindObjectOfType<TowerShop>();
	}

	public void ShowUpgrades(Tower tower) {
		_towerShop.ShowUpgrades(tower);
	}
}
