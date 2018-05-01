using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingame.towers;

public class UITooltip : MonoBehaviour {
	private static UITooltip instance;

	private RectTransform rectTransform;
	private RectTransform labelRectTransform;
	private bool sizeDirty, sizeDirtyNextFrame;

	[SerializeField] private UILabel label;

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}

		instance = this;
		rectTransform = GetComponent<RectTransform>();
		labelRectTransform = label.GetComponent<RectTransform>();
	}
	void OnDestroy() {
		if (instance == this) instance = null;
	}
	void Update() {
		if (sizeDirty) {
			sizeDirty = false;
			sizeDirtyNextFrame = true;
		} else if (sizeDirtyNextFrame) {
			Vector2 size = rectTransform.sizeDelta;
			size.y = labelRectTransform.sizeDelta.y + 10.0f;
			rectTransform.sizeDelta = size;

			sizeDirtyNextFrame = false;
		}
	}

	public static void Show(GameObject obj, Vector3 offset) {
		instance.transform.position = obj.transform.position + offset;
	}

	public static void Hide() {
		instance.transform.position = new Vector3(-5000, -5000);
	}

	public static void SetText(string text) {
		instance.label.text = text;
		instance.sizeDirty = true;
		instance.sizeDirtyNextFrame = false;
	}
}
