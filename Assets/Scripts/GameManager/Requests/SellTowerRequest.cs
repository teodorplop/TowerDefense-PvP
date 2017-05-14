using UnityEngine;
using System;

[Serializable]
public class SellTowerRequest : ActionRequest {
	[SerializeField]
	private string _player;
	[SerializeField]
	private string _tower;
	public string Player { get { return _player; } }
	public string Tower { get { return _tower; } }

	public SellTowerRequest(string player, string tower) {
		_player = player;
		_tower = tower;
	}
}
