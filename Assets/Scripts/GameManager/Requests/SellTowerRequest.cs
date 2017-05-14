using UnityEngine;

public class SellTowerRequest : ActionRequest {
	private string _tower;
	public string Tower { get { return _tower; } }
	public SellTowerRequest(string tower) {
		_tower = tower;
	}
}
