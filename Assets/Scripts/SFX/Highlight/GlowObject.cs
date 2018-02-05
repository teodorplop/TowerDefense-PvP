using UnityEngine;
using System.Collections.Generic;

namespace SFX.Glow {
	public class GlowObject : MonoBehaviour {
		private Renderer[] renderers;
		private List<Material> materials;

		public Renderer[] Renderers { get { return renderers; } }

		[SerializeField]
		private Color glowColor = Color.black;
		[SerializeField]
		private float lerpFactor = 10.0f;

		private Color currentColor, targetColor;

		private void Awake() {
			renderers = GetComponentsInChildren<Renderer>();
			materials = new List<Material>();
			foreach (Renderer renderer in renderers)
				materials.AddRange(renderer.sharedMaterials);
		}

		public void SetGlowColor(Color glowColor) {
			this.glowColor = glowColor;
		}
		public void Enable(bool enabled) {
			targetColor = enabled ? glowColor : Color.black;
			this.enabled = true;
		}

		void Update() {
			currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * lerpFactor);

			foreach (Material mat in materials)
				mat.SetColor("_GlowColor", currentColor);

			if (currentColor.Equals(targetColor))
				enabled = false;
		}
	}
}
