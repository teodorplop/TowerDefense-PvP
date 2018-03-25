using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPanel : MonoBehaviour {
	protected bool shown;

	public virtual void Show() {
		gameObject.SetActive(shown = true);
	}

	public virtual void Hide() {
		gameObject.SetActive(shown = false);
	}

	public void Toggle() {
		if (shown) Hide();
		else Show();
	}
}
