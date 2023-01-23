using UnityEngine;
using UnityEngine.UI;

public class SetTimePanel : MonoBehaviour
{
    [SerializeField] Text dayText;
    [SerializeField] Text hourText;

    public void SetTimeTexts(string day, string hour)
    {
        dayText.text = day;
        hourText.text = hour;
    }
}
