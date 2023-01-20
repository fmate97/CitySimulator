using UnityEngine;
using UnityEngine.UI;

public class UIBarScript : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int sliderMaxValue;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    void Start()
    {
        slider.maxValue = sliderMaxValue;
        slider.value = 0;
        slider.minValue = 0;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetValue(int value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetValue(int value, int maxValue)
    {
        if (slider.maxValue != maxValue)
        {
            slider.maxValue = maxValue;
        }

        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public int GetValue()
    {
        return (int)slider.value;
    }
}
