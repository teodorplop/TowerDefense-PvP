using UnityEngine;
using System;

public class Clock : MonoBehaviour {
	private static Clock instance;

	[SerializeField] private float refreshTimer = 1.0f;
	private float timer;

	void Awake() {
		if (instance != null)
			Destroy(gameObject);
		else
			instance = this;
	}
	void OnDestroy() {
		if (instance == this) instance = null;
	}

	void Update() {
		timer -= Time.deltaTime;
		if (timer <= 0.0f) {
			MacroSystem.SetMacroValue("TIME_OF_DAY", DateTime.Now.ToString(@"hh:mm tt"));
			timer = refreshTimer;
		}
	}
}
