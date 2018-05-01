using System.Collections.Generic;
using UnityEngine;
using Utils.Linq;
using Ingame.towers;

namespace Interface.towerShop {
	public class TowerShop : MonoBehaviour {
		[SerializeField]
		private TowerElement[] _prefabs;

		private List<TowerElement> _elements;
		void Awake() {
			_elements = new List<TowerElement>();
		}
		void Start() {
			foreach (var prf in _prefabs)
				prf.gameObject.SetActive(false);
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

			foreach (string upg in tower.Upgrades) {
				TowerElement prefab = _prefabs.Find(obj => obj.name == upg);
				if (prefab != null) {
					TowerElement obj = InstantiatePrefab(prefab);
					obj.Inject(towerFactory, tower);

					_elements.Add(obj);
				} else {
					Debug.LogError("Could not find TowerShopElement for " + upg, gameObject);
				}
			}
		}
	}
}
