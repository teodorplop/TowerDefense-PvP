using UnityEngine;

namespace SFX.Glow {
	[ExecuteInEditMode]
	public class GlowComposite : MonoBehaviour {
		private Material compositeMaterial;
		[SerializeField, Range(0, 10)]
		private float glowIntensity = 2.0f;

		private void Awake() {
			compositeMaterial = new Material(Shader.Find("Glowable/Composite"));
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dst) {
			compositeMaterial.SetFloat("_Intensity", glowIntensity);
			Graphics.Blit(src, dst, compositeMaterial);
		}
	}
}