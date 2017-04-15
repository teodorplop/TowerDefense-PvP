using System.Collections.Generic;

namespace GameEditor {
	public class MapDescriptionEditor : MapDescription {
		public MapDescriptionEditor(int rows, int columns) {
			_tiles = new List<List<TileDescription>>();
			Resize(rows, columns);
		}

		public delegate void OnResizeHandler();
		public event OnResizeHandler OnResizeEvent;

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

			if (OnResizeEvent != null) {
				OnResizeEvent();
			}
		}
	}
}
