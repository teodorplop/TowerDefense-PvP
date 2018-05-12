using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStats : MonoBehaviour {
	[SerializeField] private UIPlayerStat prefab;

	private List<UIPlayerStat> elems;

	void Awake() {
		elems = new List<UIPlayerStat>();
	}

	public void InjectPlayers(Player[] players) {
		UIPlayerStat elm = null;
		foreach (Player player in players) {
			elm = Instantiate(prefab);
			elm.transform.SetParent(transform);
			elm.transform.localPosition = Vector3.zero;
			elm.transform.localScale = Vector3.one;

			elm.Inject(player);
			elm.gameObject.SetActive(true);

			elems.Add(elm);
		}
	}

	public void Refresh() {
		foreach (var elm in elems)
			elm.Refresh();
	}
}
