using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [Range(1, 100)][SerializeField] int basicGameSpeed;
    [Range(1000, 2000)][SerializeField] int startingYear;
    [SerializeField] SetTimePanel timePanelScript;

    private int _gameSpeed = 1;
    private int _actualYear, _actualMonth, _actualDay, _actualHour, _actualMinute;
    private float _deltaTime = 0f;

    private enum month { January, February, March, April, May, June, July, August, September, October, November, December };

    void Start()
    {
        _actualYear = startingYear;
        _actualMonth = 3;
        _actualDay = 1;
        _actualHour = 12;
        _actualMinute = 0;

        ShowTimeInUI();
    }

    void Update()
    {
        _deltaTime += Time.deltaTime;

        if (_deltaTime >= 1f)
        {
            _deltaTime = 0f;
            AddMinute(basicGameSpeed * _gameSpeed);
            ShowTimeInUI();
        }
    }

    void ShowTimeInUI()
    {
        string day = $"{_actualDay:00} {(month)_actualMonth} {_actualYear:0000}";
        string hour = $"{_actualHour:00}:{_actualMinute:00}";
        timePanelScript.SetTimeTexts(day, hour);
    }

    public void SetGameSpeed(int speed)
    {
        if (_gameSpeed != speed)
        {
            _gameSpeed = speed;
        }
    }

    public int GetGameSpeed()
    {
        return _gameSpeed;
    }

    string GetSeasonName()
    {
        if (_actualMonth == 12 || _actualMonth == 1 || _actualMonth == 2)
        {
            return "Winter";
        }

        if (_actualMonth == 3 || _actualMonth == 4 || _actualMonth == 5)
        {
            return "Spring";
        }

        if (_actualMonth == 6 || _actualMonth == 7 || _actualMonth == 8)
        {
            return "Summer";
        }

        return "Autumn";
    }

    void AddMinute(int minute)
    {
        _actualMinute += minute;

        if (_actualMinute >= 60)
        {
            _actualMinute -= 60;
            _actualHour++;

            if (_actualHour >= 24)
            {
                _actualHour -= 24;
                AddOneDay();
            }
        }
    }

    void AddOneDay()
    {
        if (EndOfTheYear())
        {
            _actualYear++;
            _actualMonth = 1;
            _actualDay = 1;
        }
        else if (EndOfTheMonth())
        {
            _actualMonth++;
            _actualDay = 1;
        }
        else
        {
            _actualDay++;
        }
    }

    bool EndOfTheYear()
    {
        if (_actualMonth == 12 && _actualDay == 31)
        {
            return true;
        }
        return false;
    }

    bool EndOfTheMonth()
    {
        if ((_actualMonth == 1 || _actualMonth == 3 || _actualMonth == 5 || _actualMonth == 7 || _actualMonth == 8 || _actualMonth == 10 || _actualMonth == 12) && _actualDay == 31)
        {
            return true;
        }

        if ((_actualMonth == 4 || _actualMonth == 6 || _actualMonth == 9 || _actualMonth == 11) && _actualDay == 30)
        {
            return true;
        }

        if (_actualMonth == 2 && _actualDay == LastDayOfFebruary())
        {
            return true;
        }

        return false;
    }

    int LastDayOfFebruary()
    {
        if (_actualYear % 400 == 0)
        {
            return 29;
        }

        if (_actualYear % 4 == 0 && _actualYear % 100 != 0)
        {
            return 29;
        }

        return 28;
    }
}
