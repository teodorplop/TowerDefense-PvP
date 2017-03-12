using UnityEngine;

public static class TowerFactory {
	private static Transform _factoryParent;
	private static Transform FactoryParent {
		get {
			if (_factoryParent == null) {
				_factoryParent = new GameObject("TowerFactory").transform;
				_factoryParent.transform.position = Vector3.zero;
			}
			return _factoryParent;
		}
	}

	public static Tower Instantiate(string name, Vector3 worldPosition) {
		TowerBase towerBase = GameResources.Load<TowerBase>("Towers/" + name);
		if (towerBase == null) {
			Debug.LogError("Tower not found: " + name);
			return null;
		}

		return Instantiate(towerBase, worldPosition);
	}

	public static Tower Instantiate(TowerBase towerBase, Vector3 worldPosition) {
		Tower tower = new Tower(towerBase);
		TowerView towerView = Object.Instantiate(Resources.Load<TowerView>("TowerView/" + towerBase.name));
		towerView.transform.SetParent(FactoryParent);
		towerView.transform.position = worldPosition;
		towerView.Inject(tower);

		return tower;
	}
}
