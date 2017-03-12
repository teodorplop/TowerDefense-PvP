using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class MapDescription {
	[SerializeField]
	protected List<List<Tile>> _tiles;

	public int rows { get { return _tiles.Count; } }
	public int columns { get { return _tiles[0].Count; } }
	public Tile this[int i, int j] { get { return _tiles[i][j]; } }
}
