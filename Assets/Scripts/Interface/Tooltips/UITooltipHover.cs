using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] private Vector3 offset;
	[SerializeField] private string text;

	public void OnPointerEnter(PointerEventData eventData) {
		OnEnter();
		UITooltip.Show(gameObject, offset);
	}

	public void OnPointerExit(PointerEventData eventData) {
		UITooltip.Hide();
	}

	public virtual void OnEnter() {
		UITooltip.SetText(text);
	}
}
