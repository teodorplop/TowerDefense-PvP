using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField]
	private float _speed = 10.0f;
	void Update() {
		Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		transform.localPosition += Time.deltaTime * _speed * axis;
	}
}
