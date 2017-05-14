using UnityEngine;
using System;

[Serializable]
public class UpgradeTowerRequest : ActionRequest {
	[SerializeField]
	private string _player;
	[SerializeField]
	private string _tower;
	[SerializeField]
	private string _upgrade;
	public UpgradeTowerRequest(string player, string tower, string upgrade) {
		_player = player;
		_tower = tower;
		_upgrade = upgrade;
	}

	public string Player { get { return _player; } }
	public string Tower { get { return _tower; } }
	public string Upgrade { get { return _upgrade; } }

	public override string ToString() {
		return "UpgradeTowerRequest(" + _tower + ", " + _upgrade + ")";
	}
}
