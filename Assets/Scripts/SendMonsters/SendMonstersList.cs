using System;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterToSend {
	public string name;
	public int cost;

	public MonsterToSend(string name, int cost) {
		this.name = name;
		this.cost = cost;
	}
}

[Serializable]
public class SendMonstersList : IGameResource {
	public string Name { get; set; }

	[SerializeField] private List<MonsterToSend> _monsters;
	public List<MonsterToSend> Monsters { get { return _monsters; } }
}
