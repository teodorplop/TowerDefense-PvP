using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] private Vector3 offset;
	[SerializeField] protected string text;

	private bool shown;

	void OnDestroy() {
		if (shown) {
			shown = false;
			UITooltip.Hide();
		}
	}

	void OnDisable() {
		if (shown) {
			shown = false;
			UITooltip.Hide();
		}
	}

	public void OnPointerEnter(PointerEventData eventData) {
		OnEnter();
		shown = true;
		UITooltip.Show(gameObject, offset);
	}

	public void OnPointerExit(PointerEventData eventData) {
		shown = false;
		UITooltip.Hide();
	}

	public virtual void OnEnter() {
		UITooltip.SetText(text);
	}
}
