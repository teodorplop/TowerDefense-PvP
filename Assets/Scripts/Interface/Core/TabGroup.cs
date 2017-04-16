using UnityEngine;
using System.Collections.Generic;

namespace Interface {
    /// <summary>
    /// Group of tabs. Only one tab will be selected at any given moment.
    /// </summary>
    public class TabGroup : MonoBehaviour {
        [SerializeField]
        private List<Tab> tabs;
        [SerializeField]
        private Tab selectedTab;
        
        void Start() {
            foreach (Tab tab in tabs) {
                tab.Deselect();
                tab.Button.onClick.AddListener(() => Select(tab));
            }
            if (selectedTab != null) {
                selectedTab.Select();
            }
        }

        private void Select(Tab tab) {
			if (selectedTab != tab) {
				if (selectedTab != null) {
					selectedTab.Deselect();
				}
				selectedTab = tab;
				if (selectedTab != null) {
					selectedTab.Select();
				}

				if (OnSelectionChangedEvent != null) {
					OnSelectionChangedEvent(tab);
				}
			}
        }

		public void AddTab(Tab tab) {
			tab.Button.onClick.AddListener(() => Select(tab));
			tabs.Add(tab);

			if (tabs.Count == 1) {
				Select(tabs[0]);
			}
		}
		public void RemoveTab(Tab tab) {
			tab.Button.onClick.RemoveAllListeners();
			tabs.Remove(tab);

			if (tab == selectedTab && tabs.Count >= 1) {
				Select(tabs[0]);
			} else if (OnSelectionChangedEvent != null) {
				OnSelectionChangedEvent(null);
			}
		}
		
		public delegate void OnSelectionChanged(Tab tab);
		public event OnSelectionChanged OnSelectionChangedEvent;
    }
}
