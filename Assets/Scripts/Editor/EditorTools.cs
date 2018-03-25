using UnityEngine;
using UnityEditor;

public class EditorTools : MonoBehaviour {
	[MenuItem("Tools/Clear Player Prefs")]
	static void ClearPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}
}
