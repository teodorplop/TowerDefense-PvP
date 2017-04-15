using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Interface {
    /// <summary>
    /// Tab with simple OnSelect / OnDeselect events. Works well with TabGroup.
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class Tab : MonoBehaviour {
        [SerializeField]
        private UnityEvent onSelect;
        [SerializeField]
        private UnityEvent onDeselect;

        private Button button;
        public Button Button { get { return button; } }
        private Color normalColor;
        private Color selectedColor;
        void Awake() {
            button = GetComponent<Button>();
            normalColor = button.colors.normalColor;
            selectedColor = button.colors.pressedColor;
        }

        public void Select() {
            button.image.color = selectedColor;
            onSelect.Invoke();
        }
        public void Deselect() {
            button.image.color = normalColor;
            onDeselect.Invoke();
        }
    }
}
