using UnityEngine;
using UnityEngine.EventSystems;

public static class InputScanner {
	public static T ScanFor<T>(Vector3 mousePosition, LayerMask layer) where T : MonoBehaviour {
		if (CheckForUI(mousePosition)) {
			return default(T);
		}

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		if (!Physics.Raycast(ray, out hit, 1000, layer)) {
			return default(T);
		}
		return hit.collider.gameObject.GetComponent<T>();
	}

	public static bool CheckForUI(Vector3 mousePosition) {
		return EventSystem.current == null ? false : EventSystem.current.IsPointerOverGameObject();
	}

	public static bool IsOverUI() {
		return CheckForUI(Input.mousePosition);
	}
}
