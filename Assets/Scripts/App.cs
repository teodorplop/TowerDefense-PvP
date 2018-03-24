using System;
using UnityEngine;

public class App : MonoBehaviour {
	[Serializable]
	private class App_Data {
		[SerializeField] private string versionNumber = "1.00";
		[SerializeField] private string buildStage = "Alpha";

		public string VersionNumber { get { return versionNumber; } }
		public string BuildStage { get { return buildStage; } }
	}
	
	private App_Data appData;

	void Awake() {
		TextAsset asset = Resources.Load<TextAsset>("App");
		if (asset == null)
			appData = new App_Data();
		else {
			appData = JsonSerializer.Deserialize<App_Data>(asset.text);
			Resources.UnloadAsset(asset);
		}

		MacroSystem.SetMacroValue("VERSION_NUMBER", appData.VersionNumber);
		MacroSystem.SetMacroValue("BUILD_STAGE", appData.BuildStage);
	}
}
