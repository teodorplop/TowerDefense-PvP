using UnityEngine;
using System;
using System.Collections.Generic;

public class InputContext {
	private static void DoNothing(Vector3 v3) { }

	private Action<Vector3> _onMouseDown;
	public Action<Vector3> OnMouseDown { get { return _onMouseDown; } }
	private Action<Vector3> _onMouseUp;
	public Action<Vector3> OnMouseUp { get { return _onMouseUp; } }
	private Action<Vector3> _onMouse;
	public Action<Vector3> OnMouse { get { return _onMouse; } }

	public InputContext() {
		_onMouseDown = _onMouseUp = _onMouse = DoNothing;
	}
	public InputContext(Action<Vector3> onMouseDown, Action<Vector3> onMouseUp, Action<Vector3> onMouse) {
		_onMouseDown = onMouseDown != null ? onMouseDown : DoNothing;
		_onMouseUp = onMouseUp != null ? onMouseUp : DoNothing;
		_onMouse = onMouse != null ? onMouse : DoNothing;
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
		Vector3 mousePosition = Input.mousePosition;
		
		if (InputScanner.CheckForUI(mousePosition)) {
			return;
		}

		InputContext context = ActiveContext;

		if (Input.GetMouseButtonDown(0)) {
			context.OnMouseDown(mousePosition);
		}
		if (Input.GetMouseButton(0)) {
			context.OnMouse(mousePosition);
		}
		if (Input.GetMouseButtonUp(0)) {
			context.OnMouseUp(mousePosition);
		}
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
