using System.Collections.Generic;
using Utils.Linq;
using UnityEngine;

public class Player {
	private string _name;
	public string Name { get { return _name; } }

	private bool _clientPlayer;
	public bool ClientPlayer { get { return _clientPlayer; } }

	private Wallet _wallet;
	public Wallet Wallet { get { return _wallet; } }

	private Transform _transform;
	public Transform Transform { get { return _transform; } }
	public Vector3 WorldOffset { get { return _transform.position; } }

	private List<Tower> _towers;
	public List<Tower> Towers { get { return _towers; } }
	private List<Monster> _monsters;
	public List<Monster> Monsters { get { return _monsters; } }

	public Player(string name, bool clientPlayer, Wallet wallet, Transform transform) {
		_name = name;
		_clientPlayer = clientPlayer;
		_wallet = wallet;
		_transform = transform;

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

	public void Register(Monster monster) {
		monster.owner = this;
		_monsters.Add(monster);
	}
	public void Unregister(Monster monster) {
		monster.owner = null;
		_monsters.Remove(monster);
	}

	public List<Tower> GetTowersInRange(Vector3 position, float radius) {
		return _towers.Where(obj => Vector3.Distance(obj.transform.position, position) <= radius).ToList();
	}
	public List<Monster> GetMonstersInRange(Vector3 position, float radius) {
		return _monsters.Where(obj => Vector3.Distance(obj.transform.position, position) <= radius).ToList();
	}
}
