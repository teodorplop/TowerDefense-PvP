using UnityEngine;

namespace Interface {
    /// <summary>
    /// Group of tabs. Only one tab will be selected at any given moment.
    /// </summary>
    public class TabGroup : MonoBehaviour {
        [SerializeField]
        private Tab[] tabs;
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

        public void Select(Tab tab) {
            if (selectedTab != null) {
                selectedTab.Deselect();
            }
            selectedTab = tab;
            if (selectedTab != null) {
                selectedTab.Select();
            }
        }
    }
}
