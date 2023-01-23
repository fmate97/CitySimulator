using UnityEngine;
using UnityEngine.UI;

public class GameSpeedButtons : MonoBehaviour
{
    [SerializeField] TimeSystem timeSystemScript;

    [SerializeField] Button Speed_0_Button;
    [SerializeField] Button Speed_1_Button;
    [SerializeField] Button Speed_2_Button;
    [SerializeField] Button Speed_3_Button;

    private Button _selectedButton;

    void Start()
    {
        SetGameSpeed(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetGameSpeed(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGameSpeed(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGameSpeed(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGameSpeed(3);
        }
    }

    public void Speed_0_Button_Click()
    {
        SetGameSpeed(0);
    }

    public void Speed_1_Button_Click()
    {
        SetGameSpeed(1);
    }

    public void Speed_2_Button_Click()
    {
        SetGameSpeed(2);
    }

    public void Speed_3_Button_Click()
    {
        SetGameSpeed(3);
    }

    void SetGameSpeed(int gameSpeed)
    {
        if(_selectedButton != null)
        {
            _selectedButton.GetComponent<Image>().color = Color.white;
        }

        if (gameSpeed == 0) { _selectedButton = Speed_0_Button; }
        else if (gameSpeed == 1) { _selectedButton = Speed_1_Button; }
        else if (gameSpeed == 2) { _selectedButton = Speed_2_Button; }
        else if (gameSpeed == 3) { _selectedButton = Speed_3_Button; }
        else { Debug.LogError("SetGameSpeed() method get invalid gameSpeed value!"); }

        _selectedButton.GetComponent<Image>().color = Color.red;

        timeSystemScript.SetGameSpeed(gameSpeed);
    }
}
