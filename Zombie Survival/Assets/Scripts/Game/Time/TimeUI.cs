using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI daysText;
    [SerializeField] private TextMeshProUGUI mainTimeText;

    public void SetDaysText(int days)
    {
        daysText.text = "Day: " + days;
    }

    public void SetMainTime(int hours, int minutes)
    {
        if ((minutes % TimeManager.Instance.MinutesStep) == 0)
        {
            if (minutes >= 10)
                mainTimeText.text = hours + ":" + minutes;
            else
                mainTimeText.text = hours + ":0" + minutes;
        }
    }
}
