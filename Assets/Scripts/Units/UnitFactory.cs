using UnityEngine;
using Ingame.towers;
using Utils.Linq;

public class UnitFactory : MonoBehaviour {
	[SerializeField]
	private Unit[] _unitPrefabs;
	[SerializeField]
	private UnitUI _unitUIPrefab;

	public Unit InstantiateUnit(Player player, BarracksTower barracks, string unitName) {
		Unit prefab = _unitPrefabs.Find(obj => obj.name == unitName);
		if (prefab == null) {
			Debug.LogError("Unit " + unitName + " not found.");
			return null;
		}

		Unit unit = Instantiate(prefab);
		unit.transform.SetParent(barracks.transform);
		unit.transform.localScale = prefab.transform.localScale;
		player.Register(unit);

		UnitUI unitUI = Instantiate(_unitUIPrefab);
		unitUI.transform.SetParent(barracks.transform);
		unitUI.transform.localScale = unitUI.transform.localScale;
		unitUI.Inject(unit);

		return unit;
	}
}
