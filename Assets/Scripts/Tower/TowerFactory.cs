using UnityEngine;
using Utils.Linq;

public class TowerFactory : MonoBehaviour {
	[SerializeField]
	private Tower[] _towerPrefabs;

	private void ChangeTower(string tower, string upgrade) {
		Transform towerTr = transform.FindChild(tower);
		if (towerTr == null) {
			Debug.LogError("Cannot find tower " + tower, gameObject);
			return;
		}

		Tower upgradePrefab = _towerPrefabs.Find(obj => obj.name == upgrade);
		if (upgradePrefab == null) {
			Debug.LogError("Cannot find upgrade " + upgrade, gameObject);
			return;
		}

		Vector3 position = towerTr.position;
		Destroy(towerTr.gameObject);

		Tower upgradeTower = Instantiate(upgradePrefab);
		upgradeTower.transform.SetParent(transform);
		upgradeTower.transform.localScale = Vector3.one;
		upgradeTower.transform.position = position;
	}

	public void UpgradeTower(string tower, string upgrade) {
		ChangeTower(tower, upgrade);
	}

	public void DestroyTower(string tower) {
		ChangeTower(tower, "Tower");
	}
}
