using UnityEngine;
using System;

public enum TileType { Path, Constructable, Environment }

[Serializable]
public class Tile {
	[SerializeField]
	private TileType _tileType;
	public TileType tileType { get { return _tileType; } }
}
