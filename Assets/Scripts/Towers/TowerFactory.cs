using UnityEngine;
using Utils.Linq;
using System.IO;
using System.Collections.Generic;

namespace Ingame.towers {
	public class TowerFactory : MonoBehaviour {
		private static readonly string _towerAttributesPath = "TowerAttributes";

		[SerializeField]
		private Tower[] _towerPrefabs;
		private TowerAttributes[] _towerAttributes;
		private Pair<string, int>[] _towerSpotsWealth;

		void Awake() {
			_towerAttributes = new TowerAttributes[_towerPrefabs.Length];
			for (int i = 0; i < _towerPrefabs.Length; ++i) {
				TextAsset asset = Resources.Load<TextAsset>(Path.Combine(_towerAttributesPath, _towerPrefabs[i].name));
				if (asset == null) {
					Debug.LogError("Could not find tower attributes for " + _towerPrefabs[i].name);
				} else {
					_towerAttributes[i] = JsonSerializer.Deserialize<TowerAttributes>(asset.text);
					Resources.UnloadAsset(asset);
				}
			}
		}

		public void StartMatch() {
			Tower[] towers = FindObjectsOfType<Tower>();
			_towerSpotsWealth = new Pair<string, int>[towers.Length];
			for (int i = 0; i < towers.Length; ++i) {
				int prefabIdx = _towerPrefabs.IndexOf(obj => obj.name == towers[i].GetType().Name);
				towers[i].SetAttributes(_towerAttributes[prefabIdx]);
				_towerSpotsWealth[i] = new Pair<string, int>(GetTowerSpotName(towers[i].owner, towers[i].name), 0);
			}
		}

		private Tower ChangeTower(Player player, string tower, string upgrade) {
			Transform towerTr = player.Transform.FindChild(tower);
			if (towerTr == null) {
				Debug.LogError("Cannot find tower " + tower, gameObject);
				return null;
			}

			int idx = _towerPrefabs.IndexOf(obj => obj.name == upgrade);
			if (idx == -1) {
				Debug.LogError("Cannot find upgrade " + upgrade, gameObject);
				return null;
			}

			Tower upgradePrefab = _towerPrefabs[idx];
			TowerAttributes attributes = _towerAttributes[idx];

			player.Unregister(towerTr.GetComponent<Tower>());
			Vector3 position = towerTr.position;
			Destroy(towerTr.gameObject);

			Tower upgradeTower = Instantiate(upgradePrefab);
			upgradeTower.name = tower;
			upgradeTower.transform.SetParent(player.Transform);
			upgradeTower.transform.localScale = Vector3.one;
			upgradeTower.transform.position = position;
			upgradeTower.SetAttributes(attributes.Clone());
			player.Register(upgradeTower);

			return upgradeTower;
		}

		public Tower UpgradeTower(Player player, string tower, string upgrade, int cost) {
			int idx = _towerSpotsWealth.IndexOf(obj => obj.first == GetTowerSpotName(player, tower));
			_towerSpotsWealth[idx].second += cost;
			return ChangeTower(player, tower, upgrade);
		}

		public Tower DestroyTower(Player player, string tower) {
			int idx = _towerSpotsWealth.IndexOf(obj => obj.first == GetTowerSpotName(player, tower));
			_towerSpotsWealth[idx].second = 0;
			return ChangeTower(player, tower, "Tower");
		}

		public int GetUpgradeCost(Player player, string upgrade) {
			int idx = _towerPrefabs.IndexOf(obj => obj.name == upgrade);
			return _towerAttributes[idx].cost;
		}

		public float GetSellValue(Player player, string tower) {
			Tower towerObj = player.Transform.FindChild(tower).GetComponent<Tower>();
			int idx = _towerPrefabs.IndexOf(obj => obj.name == towerObj.GetType().Name);
			return _towerAttributes[idx].sellValue;
		}

		public int GetSellCost(Player player, string tower) {
			Tower towerObj = player.Transform.FindChild(tower).GetComponent<Tower>();
			int prefabIdx = _towerPrefabs.IndexOf(obj => obj.name == towerObj.GetType().Name);
			int spotIdx = _towerSpotsWealth.IndexOf(obj => obj.first == GetTowerSpotName(player, tower));

			return Mathf.FloorToInt(_towerAttributes[prefabIdx].sellValue * _towerAttributes[spotIdx].cost);
		}

		#region utils
		private static string GetTowerSpotName(Player player, string tower) {
			return player.Name + '_' + tower;
		}
		#endregion
	}
}
