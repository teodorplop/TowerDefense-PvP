using UnityEngine;

public static class GDebug {
	public static bool debug = true;

	public static void Log(object message) {
		if (debug) {
			Debug.Log(message);
		}
	}
	public static void Log(object message, Object context) {
		if (debug) {
			Debug.Log(message, context);
		}
	}
	public static void LogWarning(object message) {
		if (debug) {
			Debug.LogWarning(message);
		}
	}
	public static void LogWarning(object message, Object context) {
		if (debug) {
			Debug.LogWarning(message, context);
		}
	}
	public static void LogError(object message) {
		if (debug) {
			Debug.LogError(message);
		}
	}
	public static void LogError(object message, Object context) {
		if (debug) {
			Debug.LogError(message, context);
		}
	}
}
