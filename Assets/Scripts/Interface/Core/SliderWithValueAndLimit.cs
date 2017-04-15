
namespace Interface {
    /// <summary>
    /// Slider that displays both its current and maximum values
    /// </summary>
    public class SliderWithValueAndLimit : SliderWithValue {
        protected override void OnValueChanged(float value) {
            label.text = Value + " / " + MaxValue;
        }
    }
}
