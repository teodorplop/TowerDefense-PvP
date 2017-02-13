using System.Collections.Generic;

public class MapEditor : Map {
	public MapEditor(int rows, int columns) {
		_tiles = new List<List<Tile>>();
		Resize(rows, columns);
	}

	public void Resize(int rows, int columns) {
		if (rows < _tiles.Count) {
			_tiles.RemoveRange(rows, _tiles.Count - rows);
		} else {
			while (rows > _tiles.Count) {
				_tiles.Add(new List<Tile>());
			}
		}

		for (int i = 0; i < rows; ++i) {
			if (columns < _tiles[i].Count) {
				_tiles[i].RemoveRange(columns, _tiles[i].Count - columns);
			} else {
				while (columns > _tiles[i].Count) {
					_tiles[i].Add(new Tile());
				}
			}
		}
	}
}
