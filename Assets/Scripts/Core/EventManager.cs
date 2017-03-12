using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEvent {}

public class EventManager {
	private static EventManager _instance = null;
	public static EventManager Instance {
		get {
			if (_instance == null) {
				_instance = new EventManager();
			}
			return _instance;
		}
	}

	public delegate void EventDelegate<T>(T e) where T : GameEvent;

	private Dictionary<Type, Delegate> delegates = new Dictionary<Type, Delegate>();

	public void AddListener<T>(EventDelegate<T> del) where T : GameEvent {
		if (delegates.ContainsKey(typeof(T))) {
			Delegate tempDel = delegates[typeof(T)];

			delegates[typeof(T)] = Delegate.Combine(tempDel, del);
		} else {
			delegates[typeof(T)] = del;
		}
	}

	public void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent {
		if (delegates.ContainsKey(typeof(T))) {
			Delegate currentDel = Delegate.Remove(delegates[typeof(T)], del);

			if (currentDel == null) {
				delegates.Remove(typeof(T));
			} else {
				delegates[typeof(T)] = currentDel;
			}
		}
	}

	public void Raise(GameEvent e) {
		if (e == null) {
			Debug.Log("Invalid event argument: " + e.GetType().ToString());
			return;
		}

		if (delegates.ContainsKey(e.GetType())) {
			delegates[e.GetType()].DynamicInvoke(e);
		}
	}
}
