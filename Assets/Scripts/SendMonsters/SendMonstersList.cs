using System;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterToSend {
	public string name;
	public int cost;
}

[Serializable]
public class SendMonstersList {
	[SerializeField] private List<MonsterToSend> _monsters;
	public List<MonsterToSend> Monsters { get { return _monsters; } }
}
