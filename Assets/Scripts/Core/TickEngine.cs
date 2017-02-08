using UnityEngine;
using System.Collections.Generic;

public interface ITickable {
	void Tick();
}

public class TickEngine : MonoBehaviour {
	private static TickEngine _instance;
	private static HashSet<ITickable> _registered;
	private static HashSet<ITickable> _toUnregister;

	private static void Init() {
		if (_instance == null) {
			_instance = new GameObject("TickEngine").AddComponent<TickEngine>();
			DontDestroyOnLoad(_instance);
		}
	}
	public static void Register(ITickable tickable) {
		Init();
		_registered.Add(tickable);
	}
	public static void Unregister(ITickable tickable) {
		Init();
		_toUnregister.Add(tickable);
	}

	void Awake() {
		_registered = new HashSet<ITickable>();
		_toUnregister = new HashSet<ITickable>();
	}
	void Update() {
		foreach (ITickable tickable in _toUnregister) {
			_registered.Remove(tickable);
		}
		_toUnregister.Clear();

		foreach (ITickable tickable in _registered) {
			tickable.Tick();
		}
	}
}
