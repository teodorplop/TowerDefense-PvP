using UnityEngine;
using UnityEngine.UI;

namespace Interface {
    /// <summary>
    /// Simple slider that displays its current value
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class SliderWithValue : MonoBehaviour {
        protected Slider slider;
        public Slider Slider { get { return slider; } }

        [SerializeField]
        protected Label label;

        public float Value {
            get { return slider.value; }
            set { slider.value = value; }
        }

        public float MinValue {
            get { return slider.minValue; }
            set {
                float valueBefore = slider.value;
                slider.minValue = value;
                slider.value = valueBefore;
            }
        }

        public float MaxValue {
            get { return slider.maxValue; }
            set {
                float valueBefore = slider.value;
                slider.maxValue = value;
                slider.value = valueBefore;
                OnValueChanged(slider.value);
            }
        }

        void Awake() {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(OnValueChanged);
        }
        void OnDestroy() {
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }
        void Start() {
            OnValueChanged(slider.value);
        }

        protected virtual void OnValueChanged(float value) {
            label.text = value.ToString();
        }
    }
}
