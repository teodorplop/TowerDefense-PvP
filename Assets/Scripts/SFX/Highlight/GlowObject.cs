using UnityEngine;
using System.Collections.Generic;

namespace SFX.Glow {
	public class GlowObject : MonoBehaviour {
		private Renderer[] renderers;
		private List<Material> materials;

		public Renderer[] Renderers { get { return renderers; } }

		[SerializeField]
		private Color glowColor = Color.black;

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
			Color col = enabled ? glowColor : Color.black;
			foreach (Material mat in materials)
				mat.SetColor("_GlowColor", col);
		}
	}
}
