using UnityEngine;
using System;

public enum TileType { Environment, Path, Constructable }

[Serializable]
public class TileDescription {
	[SerializeField]
	public TileType tileType;
}
