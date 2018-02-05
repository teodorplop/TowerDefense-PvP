using UnityEngine;
using SFX.Glow;

public class HighlightManager : MonoBehaviour {
	[SerializeField]
	private Color _friendlyColor;
	[SerializeField]
	private Color _hostileColor;
	private GlowPrePass _glowPrePass;

	void Awake() {
		_glowPrePass = FindObjectOfType<GlowPrePass>();
	}

	public void Enable(GameObject obj, bool friendly) {
		GlowObject glowObj = obj.GetComponent<GlowObject>();
		if (glowObj == null) {
			Debug.LogError("No GlowObject component found on " + obj.name, obj);
			return;
		}
		glowObj.SetGlowColor(friendly ? _friendlyColor : _hostileColor);
		glowObj.Enable(true);

		_glowPrePass.SetGlowObject(glowObj);
	}
	public void Disable(GameObject obj) {
		GlowObject glowObj = obj.GetComponent<GlowObject>();
		if (glowObj == null) {
			Debug.LogError("No GlowObject component found on " + obj.name, obj);
			return;
		}
		glowObj.Enable(false);

		_glowPrePass.SetGlowObject(null);
	}
}
