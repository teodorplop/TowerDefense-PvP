using UnityEngine;
using System;
using System.Collections.Generic;

public class InputContext {
	private static void DoNothing(int m, Vector3 v) { }
	private static void DoNothing(Vector3 v) { }
	private static void DoNothing() { }

	public Action<int, Vector3> onMouseDown;
	public Action<int, Vector3> onMouseUp;
	public Action<Vector3> onMouse;
	public Action onKey;

	public InputContext() {
		onMouseDown = onMouseUp = DoNothing;
		onMouse = DoNothing;
		onKey = DoNothing;
	}
}

public class InputManager : MonoBehaviour {
	private Stack<InputContext> _contextStack;
	private InputContext ActiveContext { get { return _contextStack.Peek();	} }
	void Awake() {
		_contextStack = new Stack<InputContext>();
		_contextStack.Push(new InputContext());
	}

	void Update() {
		InputContext context = ActiveContext;
		Vector3 mousePosition = Input.mousePosition;

		if (!InputScanner.CheckForUI(mousePosition)) {
			context.onMouse(mousePosition);
			for (int i = 0; i < 3; ++i) {
				if (Input.GetMouseButtonDown(i)) {
					context.onMouseDown(i, mousePosition);
				}
				if (Input.GetMouseButtonUp(i)) {
					context.onMouseUp(i, mousePosition);
				}
			}
		}

		context.onKey();
	}

	public void PushContext(InputContext context) {
		_contextStack.Push(context);
	}
	public void PopContext() {
		if (_contextStack.Count > 1) {
			_contextStack.Pop();
		}
	}
	public void Clear() {
		_contextStack.Clear();
		_contextStack.Push(new InputContext());
	}
}
