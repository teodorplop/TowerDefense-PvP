using UnityEngine;

namespace GameEditor {
	public class TileEditorRenderer : TileRenderer {
		public static TileType selectedTileType;

		[SerializeField]
		private Color _highlightedColor;

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
			_target.tileType = selectedTileType;
			SetTile(_target);
		}
	}
}
