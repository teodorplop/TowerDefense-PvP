﻿using System.Collections.Generic;
using Pathfinding;

public class Player {
	private string _name;
	public string Name { get { return _name; } }

	private bool _clientPlayer;
	public bool ClientPlayer { get { return _clientPlayer; } }

	private Wallet _wallet;
	public Wallet Wallet { get { return _wallet; } }

	// TODO: Player does not need to retain a pathfinder.
	private Pathfinder _pathfinder;
	public Pathfinder Pathfinder { get { return _pathfinder; } }

	// TODO: Player does not need to retain a tower factory.
	private TowerFactory _towerFactory;
	public TowerFactory TowerFactory { get { return _towerFactory; } }

	private List<Tower> _towers;
	public List<Tower> Towers { get { return _towers; } }
	private List<Monster> _monsters;
	public List<Monster> Monsters { get { return _monsters; } }

	public Player(string name, bool clientPlayer, Wallet wallet, Pathfinder pathfinder, TowerFactory towerFactory) {
		_name = name;
		_clientPlayer = clientPlayer;
		_wallet = wallet;
		_pathfinder = pathfinder;
		_towerFactory = towerFactory;

		_towers = new List<Tower>();
		_monsters = new List<Monster>();
	}

	public void Register(Tower tower) {
		tower.owner = this;
		_towers.Add(tower);
	}
	public void Unregister(Tower tower) {
		tower.owner = null;
		_towers.Remove(tower);
	}
	public Tower GetTower(string name) {
		return _towers.Find(obj => obj.name == name);
	}

	public void Register(Monster monster) {
		_monsters.Add(monster);
	}
	public void Unregister(Monster monster) {
		_monsters.Remove(monster);
	}
}
