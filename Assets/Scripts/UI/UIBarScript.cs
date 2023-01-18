using UnityEngine;
using UnityEngine.UI;

public class UIBarScript : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    void Start()
    {
        slider.maxValue = 100;
        slider.value = 100;
        slider.minValue = 0;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetValue(int value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public int GetValue()
    {
        return (int)slider.value;
    }
}
