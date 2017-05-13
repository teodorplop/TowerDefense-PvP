using UnityEngine;

public class CameraYMovement : MonoBehaviour {
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _minY;
	[SerializeField]
	private float _maxY;

	public bool inputEnabled;
	void Awake() {
		inputEnabled = true;
	}

	private float GetAxisScroll() {
		return Input.GetAxis("Mouse ScrollWheel");
	}

	void Update() {
		if (inputEnabled) {
			float scrollAxis = -GetAxisScroll();

			if (scrollAxis != 0.0f) {
				float movement = Time.deltaTime * _speed * scrollAxis;

				Vector3 position = transform.localPosition;
				position.y = Mathf.Clamp(position.y + movement, _minY, _maxY);
				transform.localPosition = position;
			}
		}
	}
}
