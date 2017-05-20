using System.Diagnostics;
using System.Collections.Generic;

/// <summary>
/// Useful when you want to track how long some functions take to complete.
/// </summary>
public class UnityWatch {
	private class UnityStopWatch {
		public string name;
		public Stopwatch watch;
		public UnityStopWatch(string name, Stopwatch watch) {
			this.name = name;
			this.watch = watch;
		}
	}

	private static Stack<UnityStopWatch> _watches = new Stack<UnityStopWatch>();
	/// <summary>
	/// Starts a new watch, with a name.
	/// </summary>
	public static void Start(string name) {
		_watches.Push(new UnityStopWatch(name, Stopwatch.StartNew()));
	}
	/// <summary>
	/// Works with Start function. Stops the last watch started, and prints the time elapsed in ms.
	/// </summary>
	public static void Stop() {
		if (_watches.Count == 0) {
			UnityEngine.Debug.LogWarning("UnityWatch: You must call Start first.");
			return;
		}

		UnityStopWatch watch = _watches.Pop();
		watch.watch.Stop();
		UnityEngine.Debug.Log("UnityWatch: " + watch.name + " " + watch.watch.ElapsedMilliseconds + " ms.");
	}
}
