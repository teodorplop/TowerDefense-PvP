using System.Collections.Generic;

namespace GameEditor {
	public class MapEditorRenderer : MapRenderer {
		private MapDescriptionEditor _mapDescriptionEditor;

		public override void Initialize(MapDescription mapDescription) {
			base.Initialize(mapDescription);
			_mapDescriptionEditor = mapDescription as MapDescriptionEditor;
		}

		public void OnMapResized() {
			if (_mapDescription.rows < _tiles.Count) {
				for (int i = _mapDescription.rows; i < _tiles.Count; ++i) {
					foreach (TileRenderer tr in _tiles[i]) {
						Destroy(tr.gameObject);
					}
				}
				_tiles.RemoveRange(_mapDescription.rows, _tiles.Count - _mapDescription.rows);
			} else {
				while (_mapDescription.rows > _tiles.Count) {
					_tiles.Add(new List<TileRenderer>());
				}
			}

			for (int i = 0; i < _tiles.Count; ++i) {
				if (_mapDescription.columns < _tiles[i].Count) {
					for (int j = _mapDescription.columns; j < _tiles[i].Count; ++j) {
						Destroy(_tiles[i][j].gameObject);
					}
					_tiles[i].RemoveRange(_mapDescription.columns, _tiles[i].Count - _mapDescription.columns);
				} else {
					while (_mapDescription.columns > _tiles[i].Count) {
						TileRenderer obj = InstantiateTileRenderer(_mapDescription[i, _tiles[i].Count], i, _tiles[i].Count);
						_tiles[i].Add(obj);
					}
				}
			}
		}
	}
}
