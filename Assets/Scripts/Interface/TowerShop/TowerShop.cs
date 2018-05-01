using System.Collections.Generic;
using UnityEngine;
using Ingame.towers;

namespace Interface.towerShop {
	public class TowerShop : MonoBehaviour {
		[SerializeField] private BuyTowerElement _buyTowerPrefab;
		[SerializeField] private RallyPointElement _rallyPointPrefab;
		[SerializeField] private SellTowerElement _sellTowerPrefab;

		private List<TowerElement> _elements;
		void Awake() {
			_elements = new List<TowerElement>();
		}
		void Start() {
			_buyTowerPrefab.gameObject.SetActive(false);
			_sellTowerPrefab.gameObject.SetActive(false);
			_rallyPointPrefab.gameObject.SetActive(false);
		}

		private void Clear() {
			foreach (TowerElement elm in _elements) {
				Destroy(elm.gameObject);
			}
			_elements.Clear();
		}

		private TowerElement InstantiatePrefab(TowerElement prefab) {
			TowerElement obj = Instantiate(prefab);
			obj.transform.SetParent(transform);
			obj.transform.localScale = Vector3.one;
			obj.gameObject.SetActive(true);
			return obj;
		}
		public void ShowUpgrades(TowerFactory towerFactory, Tower tower) {
			Clear();

			if (tower == null) {
				return;
			}

			TowerElement elm;
			foreach (string upg in tower.Upgrades) {
				if (upg == "SellTower")
					elm = _sellTowerPrefab;
				else if (upg == "RallyPoint")
					elm = _rallyPointPrefab;
				else
					elm = _buyTowerPrefab;

				elm = InstantiatePrefab(elm);
				elm.Inject(towerFactory, tower, upg);
				_elements.Add(elm);
			}
		}
	}
}
