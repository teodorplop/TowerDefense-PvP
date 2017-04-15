using UnityEngine;

public class CameraAxisMovement : MonoBehaviour {
	[SerializeField]
	private float _speed;

	public bool inputEnabled;
	void Awake() {
		inputEnabled = true;
	}

	private Vector2 GetAxisMovement() {
		return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	void Update() {
		if (inputEnabled) {
			Vector2 axis = GetAxisMovement();
			Vector3 movement = Time.deltaTime * _speed * new Vector3(axis.x, 0, axis.y);

			transform.localPosition += movement;
		}
	}
}
