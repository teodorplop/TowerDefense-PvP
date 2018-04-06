using UnityEngine;

namespace Interface {
	public class TimerLabel : Label {
		private bool _running;
		private float _time;

		public void Play() {
			_running = true;
		}
		public void Reset() {
			_time = 0.0f;
			text = _time.ToString("0.0 s");
		}
		public void Pause() {
			_running = false;
		}
		public void Stop() {
			Pause();
			Reset();
		}

		void Update() {
			if (_running) {
				_time += Time.deltaTime;
				text = _time.ToString("0.0 s");
			}
		}
	}
}
