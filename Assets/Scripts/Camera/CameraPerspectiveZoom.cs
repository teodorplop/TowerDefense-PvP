using UnityEngine;

public class CameraPerspectiveZoom : MonoBehaviour {
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _minFieldOfView;
	[SerializeField]
	private float _maxFieldOfView;
	[SerializeField]
	private float _smoothTime;

	private Camera _camera;
	private float _zoomVelocity, _currentZoomVelocity;
	private bool _zoomInertia;

	public bool inputEnabled;

	void Awake() {
		_camera = GetComponent<Camera>();
		inputEnabled = true;
	}

	private float GetAxisScroll() {
		return Input.GetAxis("Mouse ScrollWheel");
	}

	void Update() {
		if (inputEnabled) {
			float scrollAxis = -GetAxisScroll();

			if (scrollAxis != 0.0f) {
				_zoomVelocity = Time.deltaTime * _speed * scrollAxis;
				_camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView + _zoomVelocity, _minFieldOfView, _maxFieldOfView);
				
				_zoomInertia = true;
				_currentZoomVelocity = 0.0f;
			}
		}

		if (_zoomInertia && Mathf.Abs(_zoomVelocity) > 0.5f) {
			_camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView + _zoomVelocity, _minFieldOfView, _maxFieldOfView);
			_zoomVelocity = Mathf.SmoothDamp(_zoomVelocity, 0.0f, ref _currentZoomVelocity, _smoothTime);
		} else {
			_zoomInertia = false;
		}
	}
}
