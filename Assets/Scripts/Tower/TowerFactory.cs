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

}
