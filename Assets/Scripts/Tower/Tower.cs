using UnityEngine;

public class Tower : MonoBehaviour {
	protected float _timer;

	// We used fixed update to make sure towers attack at regular intervals.
	protected virtual void FixedUpdate() {
		_timer += Time.fixedDeltaTime;
	}
}
