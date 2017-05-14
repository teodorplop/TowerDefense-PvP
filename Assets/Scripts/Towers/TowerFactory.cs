using UnityEngine;
using Utils.Linq;

public class TowerFactory : MonoBehaviour {
	[SerializeField]
	private Tower[] _towerPrefabs;

	private Tower ChangeTower(Player player, string tower, string upgrade) {
		Transform towerTr = player.Transform.FindChild(tower);
		if (towerTr == null) {
			Debug.LogError("Cannot find tower " + tower, gameObject);
			return null;
		}

		Tower upgradePrefab = _towerPrefabs.Find(obj => obj.name == upgrade);
		if (upgradePrefab == null) {
			Debug.LogError("Cannot find upgrade " + upgrade, gameObject);
			return null;
		}

		player.Unregister(towerTr.GetComponent<Tower>());
		Vector3 position = towerTr.position;
		Destroy(towerTr.gameObject);

		Tower upgradeTower = Instantiate(upgradePrefab);
		upgradeTower.transform.SetParent(player.Transform);
		upgradeTower.transform.localScale = Vector3.one;
		upgradeTower.transform.position = position;
		player.Register(upgradeTower);

		return upgradeTower;
	}

	public Tower UpgradeTower(Player player, string tower, string upgrade) {
		return ChangeTower(player, tower, upgrade);
	}

	public Tower DestroyTower(Player player, string tower) {
		return ChangeTower(player, tower, "Tower");
	}
}
