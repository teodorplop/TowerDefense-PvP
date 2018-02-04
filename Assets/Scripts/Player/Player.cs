using System.Collections.Generic;
using Utils.Linq;
using UnityEngine;
using Ingame.towers;

public class Player {
	public bool isActive;

	[SerializeField]
	private string _name;
	public string Name { get { return _name; } }

	private bool _clientPlayer;
	public bool ClientPlayer { get { return _clientPlayer; } }

	private Wallet _wallet;
	public Wallet Wallet { get { return _wallet; } }

	private Transform _transform;
	public Transform Transform { get { return _transform; } }
	public Vector3 WorldOffset { get { return _transform.position; } }

	private Terrain _terrain;
	public Terrain Terrain { get { return _terrain; } }

	private List<Tower> _towers;
	public List<Tower> Towers { get { return _towers; } }
	private List<Monster> _monsters;
	public List<Monster> Monsters { get { return _monsters; } }
	private List<Unit> _units;
	public List<Unit> Units { get { return _units; } }

	public Player(string name, bool clientPlayer, Wallet wallet, Transform transform) {
		isActive = true;

		_name = name;
		_clientPlayer = clientPlayer;
		_wallet = wallet;
		_transform = transform;
		_terrain = _transform.GetComponentInChildren<Terrain>();

		_towers = new List<Tower>();
		_monsters = new List<Monster>();
		_units = new List<Unit>();
	}

	public void Register(Tower tower) {
		tower.owner = this;
		if (!_towers.Contains(tower)) {
			_towers.Add(tower);
		}
	}
	public void Unregister(Tower tower) {
		tower.owner = null;
		_towers.Remove(tower);
	}

	public void Register(Monster monster) {
		monster.owner = this;
		if (!_monsters.Contains(monster)) {
			_monsters.Add(monster);
		}
	}
	public void Unregister(Monster monster) {
		monster.owner = null;
		_monsters.Remove(monster);
	}

	public void Register(Unit unit) {
		if (unit != null) {
			unit.owner = this;
			if (!_units.Contains(unit)) {
				_units.Add(unit);
			}
		}
	}
	public void Unregister(Unit unit) {
		if (unit != null) {
			unit.owner = null;
			_units.Remove(unit);
		}
	}

	public List<Tower> GetTowersInRange(Vector3 position, float radius) {
		return _towers.Where(obj => Vector3Utils.PlanarDistance(obj.transform.position, position) <= radius).ToList();
	}
	public List<Monster> GetMonstersInRange(Vector3 position, float radius) {
		return _monsters.Where(obj => Vector3Utils.PlanarDistance(obj.transform.position, position) <= radius).ToList();
	}
}
