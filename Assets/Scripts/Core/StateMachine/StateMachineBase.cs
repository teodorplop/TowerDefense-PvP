using System;
using UnityEngine;
using System.Reflection;

/// <summary>
/// Inherit this to have a state machine like behaviour. If you need to implement a separate Awake or Update functions, override OnAwake and OnUpdate functions.
/// </summary>
public abstract class StateMachineBase : MonoBehaviour {
	protected StateMachineHandler _stateMachineHandler;
	void Awake() {
		_stateMachineHandler = gameObject.AddComponent<StateMachineHandler>();
		OnAwake();
	}

	protected virtual void OnAwake() {}
	protected virtual void OnUpdate() {}

	private Enum _currentState;
	public Enum currentState {
		get { return _currentState; }
		set {
			_currentState = value;
			ConfigureCurrentState();
		}
	}

	#region actions
	private static void DoNothing() {}
	private static void DoNothing(Vector3 mi) {}
	private static void DoNothingCollider(Collider other) {}
	private static void DoNothingCollision(Collision other) {}

	[HideInInspector] private Action DoUpdate = DoNothing;
	[HideInInspector] private Action DoLateUpdate = DoNothing;
	[HideInInspector] private Action DoFixedUpdate = DoNothing;
	[HideInInspector] private Action<Collider> DoOnTriggerEnter = DoNothingCollider;
	[HideInInspector] private Action<Collider> DoOnTriggerStay = DoNothingCollider;
	[HideInInspector] private Action<Collider> DoOnTriggerExit = DoNothingCollider;
	[HideInInspector] private Action<Collision> DoOnCollisionEnter = DoNothingCollision;
	[HideInInspector] private Action<Collision> DoOnCollisionStay = DoNothingCollision;
	[HideInInspector] private Action<Collision> DoOnCollisionExit = DoNothingCollision;
	[HideInInspector] private Action DoOnMouseEnter = DoNothing;
	[HideInInspector] private Action DoOnMouseUp = DoNothing;
	[HideInInspector] private Action DoOnMouseDown = DoNothing;
	[HideInInspector] private Action DoOnMouseOver = DoNothing;
	[HideInInspector] private Action DoOnMouseExit = DoNothing;
	[HideInInspector] private Action DoOnMouseDrag = DoNothing;
	[HideInInspector] private Action DoOnGUI = DoNothing;
	#endregion

	#region configuration
	private void ConfigureCurrentState() {
		DoUpdate = ConfigureDelegate<Action>("Update", DoNothing);
		DoLateUpdate = ConfigureDelegate<Action>("LateUpdate", DoNothing);
		DoFixedUpdate = ConfigureDelegate<Action>("FixedUpdate", DoNothing);
		DoOnTriggerEnter = ConfigureDelegate<Action<Collider>>("OnTriggerEnter", DoNothingCollider);
		DoOnTriggerStay = ConfigureDelegate<Action<Collider>>("OnTriggerStay", DoNothingCollider);
		DoOnTriggerExit = ConfigureDelegate<Action<Collider>>("OnTriggerExit", DoNothingCollider);
		DoOnCollisionEnter = ConfigureDelegate<Action<Collision>>("OnCollisionEnter", DoNothingCollision);
		DoOnCollisionStay = ConfigureDelegate<Action<Collision>>("OnCollisionStay", DoNothingCollision);
		DoOnCollisionExit = ConfigureDelegate<Action<Collision>>("OnCollisionExit", DoNothingCollision);
		DoOnMouseEnter = ConfigureDelegate<Action>("OnMouseEnter", DoNothing);
		DoOnMouseUp = ConfigureDelegate<Action>("OnMouseUp", DoNothing);
		DoOnMouseDown = ConfigureDelegate<Action>("OnMouseDown", DoNothing);
		DoOnMouseOver = ConfigureDelegate<Action>("OnMouseOver", DoNothing);
		DoOnMouseDrag = ConfigureDelegate<Action>("OnMouseDrag", DoNothing);
		DoOnMouseExit = ConfigureDelegate<Action>("OnMouseExit", DoNothing);
		DoOnGUI = ConfigureDelegate<Action>("OnGUI", DoNothing);

		useGUILayout = DoOnGUI != DoNothing;
	}
	
	public T ConfigureField<T>(string fieldRoot, T Default) {
		FieldInfo field = GetType().GetField(_currentState.ToString() + "_" + fieldRoot, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		if (field != null) {
			return (T)field.GetValue(this);
		}
		return Default;
	}
	public T ConfigureDelegate<T>(string methodRoot, T Default) where T : class {
		MethodInfo method = GetType().GetMethod(_currentState.ToString() + "_" + methodRoot, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);

		if (method != null) {
			return Delegate.CreateDelegate(typeof(T), this, method) as T;
		}

		return Default;
	}
	#endregion

	#region functions
	void Update() {
		DoUpdate();
		OnUpdate();
	}
	void LateUpdate() {
		DoLateUpdate();
	}
	void FixedUpdate() {
		DoFixedUpdate();
	}
	void OnTriggerEnter(Collider other) {
		DoOnTriggerEnter(other);
	}
	void OnTriggerStay(Collider other) {
		DoOnTriggerStay(other);
	}
	void OnTriggerExit(Collider other) {
		DoOnTriggerExit(other);
	}
	void OnCollisionEnter(Collision other) {
		DoOnCollisionEnter(other);
	}
	void OnCollisionStay(Collision other) {
		DoOnCollisionStay(other);
	}
	void OnCollisionExit(Collision other) {
		DoOnCollisionExit(other);
	}
	void OnMouseEnter() {
		DoOnMouseEnter();
	}
	void OnMouseUp() {
		DoOnMouseUp();
	}
	void OnMouseDown() {
		DoOnMouseDown();
	}
	void OnMouseOver() {
		DoOnMouseOver();
	}
	void OnMouseDrag() {
		DoOnMouseDrag();
	}
	void OnMouseExit() {
		DoOnMouseExit();
	}
	void OnGUI() {
		DoOnGUI();
	}
	#endregion
}
