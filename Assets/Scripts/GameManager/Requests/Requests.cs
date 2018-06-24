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

[Serializable]
public class UpgradeTowerRequest : ActionRequest {
	[SerializeField]
	private string _player;
	[SerializeField]
	private string _tower;
	[SerializeField]
	private string _upgrade;
	[SerializeField]
	private double _delay;
	[SerializeField]
	private bool _hasDelay;
	public UpgradeTowerRequest(string player, string tower, string upgrade) {
		_player = player;
		_tower = tower;
		_upgrade = upgrade;
		_hasDelay = false;
	}
	public UpgradeTowerRequest(string player, string tower, string upgrade, double delay) {
		_player = player;
		_tower = tower;
		_upgrade = upgrade;
		_delay = delay;
		_hasDelay = true;
	}

	public string Player { get { return _player; } }
	public string Tower { get { return _tower; } }
	public string Upgrade { get { return _upgrade; } }
	public double Delay { get { return _delay; } }
	public bool HasDelay { get { return _hasDelay; } }

	public override string ToString() {
		return "UpgradeTowerRequest(" + _tower + ", " + _upgrade + ")";
	}
}

[Serializable]
public class SendMonstersRequest : ActionRequest {
	public override string ToString() {
		return "SendMonstersRequest()";
	}
}

[Serializable]
public class SendMonsterRequest : ActionRequest {
	[SerializeField] private string playerOwner;
	[SerializeField] private MonsterToSend monster;

	public SendMonsterRequest(string playerOwner, MonsterToSend monster) {
		this.playerOwner = playerOwner;
		this.monster = monster;
	}

	public string PlayerOwner { get { return playerOwner; } }
	public MonsterToSend Monster { get { return monster; } }

	public override string ToString() {
		return "SendMonsterRequest(" + monster.name + ", " + monster.cost + ')';
	}
}