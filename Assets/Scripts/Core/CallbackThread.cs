using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CallbackThread : MonoBehaviour {
	private static CallbackThread instance;

	private List<Action> callbacks;

	void Awake() {
		callbacks = new List<Action>();
	}
	void OnDestroy() {
		if (instance == this)
			instance = null;
	}

	private static void Init() {
		if (instance == null) {
			instance = new GameObject("CallbackThread").AddComponent<CallbackThread>();
			DontDestroyOnLoad(instance);
		}
	}

	public static void Run(Action method, Action callback) {
		Init();
		ThreadPool.QueueUserWorkItem(arg => {
			if (method != null) method();
			lock (instance.callbacks) {
				instance.callbacks.Add(callback);
			}
		});
	}

	void Update() {
		lock (callbacks) {
			for (int i = 0; i < callbacks.Count; ++i) {
				if (callbacks[i] != null) callbacks[i]();
			}
			callbacks.Clear();
		}
	}
}
