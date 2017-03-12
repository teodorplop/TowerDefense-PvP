using UnityEngine;
using System;

public enum TileType { Environment, Path, Constructable }

[Serializable]
public class Tile {
	[SerializeField]
	public TileType tileType;
}
