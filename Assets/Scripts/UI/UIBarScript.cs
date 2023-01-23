using UnityEngine;
using UnityEngine.UI;

public class UIBarScript : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int sliderMaxValue;

    void Start()
    {
        slider.maxValue = sliderMaxValue;
        slider.value = 0;
        slider.minValue = 0;
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }

    public void SetValue(int value, int maxValue)
    {
        if (slider.maxValue != maxValue)
        {
            slider.maxValue = maxValue;
        }

        slider.value = value;
    }

    public int GetValue()
    {
        return (int)slider.value;
    }
}
