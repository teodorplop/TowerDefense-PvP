using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace TMPro.EditorUtilities {
	public static class UIContextEditor {
		[MenuItem("GameObject/TowerDefense/Label UI", false, 0)]
		private static void LabelUI(MenuCommand command) {
			GameObject obj = new GameObject();
			obj.name = "New Label";
			obj.layer = LayerMask.NameToLayer("UI");

			TextMeshProUGUI tmPro = obj.AddComponent<TextMeshProUGUI>();
			tmPro.alignment = TextAlignmentOptions.Center;
			tmPro.richText = false;
			tmPro.raycastTarget = false;
			tmPro.text = "New Label";

			obj.AddComponent<Interface.Label>();

			SetParent(obj, command.context);
		}

		private static void SetParent(GameObject obj, Object context) {
			if (context != null) {
				obj.transform.SetParent((context as GameObject).transform, true);
				obj.transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}
}
