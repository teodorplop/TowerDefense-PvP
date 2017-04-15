namespace GameEditor {
	public class TileEditorRenderer : TileRenderer {
		public static TileType selectedTileType;
		
		public void SetTileType() {
			_tile.tileType = selectedTileType;
			SetTile(_tile);
		}

		void OnMouseEnter() {
			if (!InputScanner.IsOverUI()) {
				_renderer.material.color = _highlightedColor;
			}
		}
		void OnMouseOver() {
			_renderer.material.color = InputScanner.IsOverUI() ? _color : _highlightedColor;
		}
		void OnMouseExit() {
			_renderer.material.color = _color;
		}
	}
}
