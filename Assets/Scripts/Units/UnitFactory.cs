using UnityEngine;
using Ingame.towers;
using Utils.Linq;

public class UnitFactory : MonoBehaviour {
	[SerializeField]
	private Unit[] _unitPrefabs;
	private UnitAttributes[] _unitAttributes;
	[SerializeField]
	private UnitUI _unitUIPrefab;

	void Awake() {
		_unitAttributes = new UnitAttributes[_unitPrefabs.Length];
		for (int i = 0; i < _unitPrefabs.Length; ++i) {
			_unitAttributes[i] = GameResources.LoadUnitAttributes(_unitPrefabs[i].name);
		}
	}

	public Unit InstantiateUnit(Player player, BarracksTower barracks, string unitName) {
		int idx = _unitPrefabs.IndexOf(obj => obj.name == unitName);
		if (idx == -1) {
			Debug.LogError("Unit " + unitName + " not found.");
			return null;
		}

		Unit prefab = _unitPrefabs[idx];
		UnitAttributes attributes = _unitAttributes[idx];

		Unit unit = Instantiate(prefab);
		unit.transform.SetParent(barracks.transform);
		unit.transform.localScale = prefab.transform.localScale;
		player.Register(unit);
		unit.SetAttributes(attributes);

		UnitUI unitUI = Instantiate(_unitUIPrefab);
		unitUI.transform.SetParent(barracks.transform);
		unitUI.transform.localScale = unitUI.transform.localScale;
		unitUI.Inject(unit);

		return unit;
	}

	public string GetUnitUISprite(string name) {
		int idx = _unitPrefabs.IndexOf(obj => obj.name == name);
		return idx != -1 ? _unitAttributes[idx].uiSprite : "";
	}
}
