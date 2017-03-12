using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Utils.Linq;
using System;

namespace GameEditor.Interface {
	public class TileSettingsPanel : MonoBehaviour {
		[SerializeField]
		private ToggleGroup _toggleGroup;
		private IEnumerable<Toggle> _toggles;
		void Awake() {
			_toggles = _toggleGroup.ActiveToggles();
		}

		public void OnToggleChanged() {
			Toggle active = _toggles.Find(obj => obj.isOn);
			if (active != null) {
				TileEditorRenderer.selectedTileType = (TileType)Enum.Parse(typeof(TileType), active.name);
			}
		}
	}
}
