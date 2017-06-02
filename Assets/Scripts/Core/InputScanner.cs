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

	public static bool PlanePosition(Vector3 mousePosition, out Vector3 hitPosition) {
		hitPosition = Vector3.zero;
		if (CheckForUI(mousePosition)) {
			return false;
		}

		Plane plane = new Plane(Vector3.up, Vector3.zero);
		Ray inputRay = Camera.main.ScreenPointToRay(mousePosition);
		float distance;

		if (plane.Raycast(inputRay, out distance)) {
			hitPosition = inputRay.GetPoint(distance);
			return true;
		}
		return false;
	}

	public static bool CheckForUI(Vector3 mousePosition) {
		return EventSystem.current == null ? false : EventSystem.current.IsPointerOverGameObject();
	}

	public static bool IsOverUI() {
		return CheckForUI(Input.mousePosition);
	}
}
