using TMPro;
using UnityEngine;
using Interface;

public class UILabel : Label {
    [SerializeField] private bool macro;
	[SerializeField] private string id;
    [SerializeField] private string macroText;

    void Awake() {
		macroText = Strings.GetText(id = label.text);
        if (macro) MacroSystem.Register(UpdateText, macroText);
    }
    void Start() {
        UpdateText();
    }
    void OnDestroy() {
        if (macro) MacroSystem.Unregister(UpdateText, macroText);
    }

	public override string text {
		get { return base.text; }
		set {
			if (id != value) {
				if (macro) MacroSystem.Unregister(UpdateText, macroText);
				
				macroText = Strings.GetText(id = value);
				UpdateText();

				if (macro) MacroSystem.Register(UpdateText, macroText);
			}
			macroText = Strings.GetText(value);
			base.text = value;
		}
	}

	private void UpdateText() {
        label.text = macro ? MacroSystem.TranslateMacros(macroText) : macroText;
    }
}
