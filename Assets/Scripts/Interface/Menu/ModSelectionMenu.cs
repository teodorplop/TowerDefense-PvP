using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModSelectionMenu : MonoBehaviour {
	[SerializeField] private TMP_Dropdown dropdown;

	private Mod[] mods;

	public Mod SelectedMod { get { return mods[dropdown.value]; } }

	void Start() {
		mods = Modding.Instance.Mods;

		dropdown.ClearOptions();
		foreach (Mod mod in mods)
			dropdown.options.Add(new TMP_Dropdown.OptionData(mod.Name));
		dropdown.RefreshShownValue();
	}
}
