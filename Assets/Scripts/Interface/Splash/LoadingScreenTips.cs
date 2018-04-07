using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenTips : MonoBehaviour {
	[SerializeField] private string loadingScreenPrefix = "STR_loadingScreenTip";
	[SerializeField] private UILabel tipsLabel;

	private System.Random random;
	private List<string> tips;
	private int tipIdx;

	void Awake() {
		random = new System.Random();
		tips = new List<string>();

		int i = 0;
		string tipId = loadingScreenPrefix + i.ToString();
		while (Strings.GetText(tipId) != tipId) {
			tips.Add(tipId);
			++i;
			tipId = loadingScreenPrefix + i.ToString();
		}
	}
	void Start() {
		NextTip();
	}

	public void NextTip() {
		if (tips.Count > 0) {
			tipIdx = random.Next(tips.Count);
			tipsLabel.text = tips[tipIdx];
		}
	}
}
