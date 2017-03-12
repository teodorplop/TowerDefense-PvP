using UnityEngine;

namespace GameEditor {
	public class TileEditorRenderer : TileRenderer {
		public static TileType selectedTileType;

		void OnMouseEnter() {
			if (Input.GetMouseButton(0)) {
				OnMouseDown();
			}
			_renderer.material.color = _highlightedColor;
		}
		void OnMouseExit() {
			_renderer.material.color = _color;
		}
		void OnMouseDown() {
			_tile.tileType = selectedTileType;
			SetTile(_tile);
		}
	}
}
