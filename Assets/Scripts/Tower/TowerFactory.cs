using UnityEngine;

public class TowerFactory {
	public Tower Instantiate(string name) {
		TowerBase towerBase = GameResources.Load<TowerBase>("Towers/ArrowTower");
		if (towerBase == null) {
			Debug.LogError("Tower not found: " + name);
			return null;
		}

		return new Tower(towerBase);
	}
}
