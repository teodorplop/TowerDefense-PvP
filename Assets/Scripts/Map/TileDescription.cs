using UnityEngine;
using System;

public enum TileType { Environment, Reachable, Constructable }

[Serializable]
public class TileDescription {
	[SerializeField]
	public TileType tileType;
}
