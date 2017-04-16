using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class MapDescription {
	[SerializeField]
	protected List<List<TileDescription>> _tiles = new List<List<TileDescription>>();
	[SerializeField]
	protected Dictionary<string, PathDescription> _paths = new Dictionary<string, PathDescription>();

	public int rows { get { return _tiles.Count; } }
	public int columns { get { return _tiles[0].Count; } }
	public TileDescription this[int i, int j] { get { return _tiles[i][j]; } }
	public PathDescription GetPath(string name) {
		if (!_paths.ContainsKey(name)) {
			Debug.LogError("Path " + name + " not found.");
			return new PathDescription();
		}
		return _paths[name];
	}
}
