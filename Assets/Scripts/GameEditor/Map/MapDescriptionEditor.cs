using System.Collections.Generic;
using UnityEngine;

namespace GameEditor {
	public class MapDescriptionEditor : MapDescription {
		public MapDescriptionEditor(int rows, int columns) {
			Resize(rows, columns);
		}
		public void Resize(int rows, int columns) {
			if (rows < _tiles.Count) {
				_tiles.RemoveRange(rows, _tiles.Count - rows);
			} else {
				while (rows > _tiles.Count) {
					_tiles.Add(new List<TileDescription>());
				}
			}

			for (int i = 0; i < rows; ++i) {
				if (columns < _tiles[i].Count) {
					_tiles[i].RemoveRange(columns, _tiles[i].Count - columns);
				} else {
					while (columns > _tiles[i].Count) {
						_tiles[i].Add(new TileDescription());
					}
				}
			}
		}

		public void AddPath(string pathName) {
			_paths.Add(pathName, new PathDescriptionEditor());
		}
		public void RemovePath(string pathName) {
			_paths.Remove(pathName);
		}
	}
}
