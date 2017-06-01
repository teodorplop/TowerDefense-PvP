using UnityEngine;

namespace SFX.Glow {
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class GlowPrePass : MonoBehaviour {
		[SerializeField]
		private int blurIterations = 4;
		[SerializeField, Tooltip("Will not update in play mode.")]
		private int blurDownsample = 1;

		private new Camera camera;
		private Shader glowShader;
		private RenderTexture prePass;
		private RenderTexture blur;
		private Material blurMaterial;

		private void Awake() {
			camera = GetComponent<Camera>();

			glowShader = Shader.Find("Glowable/GlowReplace");

			prePass = new RenderTexture(Screen.width, Screen.height, 24);
			blur = new RenderTexture(Screen.width >> blurDownsample, Screen.height >> blurDownsample, 0);

			Shader.SetGlobalTexture("_GlowPrePassTex", prePass);
			Shader.SetGlobalTexture("_GlowBlurTex", blur);

			blurMaterial = new Material(Shader.Find("Glowable/Blur"));
			blurMaterial.SetVector("_BlurSize", new Vector2(blur.texelSize.x * 1.5f, blur.texelSize.y * 1.5f));

			camera.targetTexture = prePass;
			camera.SetReplacementShader(glowShader, "Glowable");
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dst) {
			Graphics.Blit(src, blur);
			RenderTexture temp = RenderTexture.GetTemporary(blur.width, blur.height);
			for (int i = 0; i < blurIterations; ++i) {
				Graphics.Blit(blur, temp, blurMaterial);
				Graphics.Blit(temp, blur);
			}
			RenderTexture.ReleaseTemporary(temp);

			// src is texture returned by the rendering process, dst is texture we can modify and return
			// Render without modifying to our dst texture, which is actually the prePass texture
			Graphics.Blit(src, dst);
		}
	}
}
