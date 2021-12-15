using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeUI))]
public class TimeManager : MonoBehaviour
{
    [SerializeField] private int startMinutes;
    [SerializeField] private int startHours = 7;
    [SerializeField] private int startDays = 1;
    [SerializeField] private float gameDeltaTime = 0.7f;

    [Space(7)]
    [SerializeField] private float currentSeconds;
    [SerializeField] private int currentMinutes;
    [SerializeField] private int currentHours;
    [SerializeField] private int currentDays;

    private TimeUI ui;

    public float MinutesStep = 10;

    public static TimeManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }

    private void Start()
    {
        ui = GetComponent<TimeUI>();
        SetStartTime();
    }

    private void Update()
    {
        Timer();
    }

    private void SetStartTime()
    {
        ui.SetDaysText(startDays);
        ui.SetMainTime(startHours, startMinutes);
        currentMinutes = startMinutes;
        currentHours = startHours;
        currentDays = startDays;
    }

    private void Timer()
    {
        currentSeconds += gameDeltaTime;
        if (currentSeconds >= 60)
        {
            currentMinutes++;
            currentSeconds = 0;
            ui.SetMainTime(currentHours, currentMinutes);
            ActionsPerMinute();
        }

        if (currentMinutes >= 60)
        {
            currentHours++;
            currentMinutes = 0;
            ui.SetMainTime(currentHours, currentMinutes);
            ActionsPerHour();
        }

        if (currentHours >= 24)
        {
            currentDays++;
            currentHours = 0;
            ui.SetDaysText(currentDays);
            ActionsPerDay();
        }
    }

    private void ActionsPerMinute()
    {
        PlayerNeedsController.Instance.ChangeNeedsPerMinute();
    }

    private void ActionsPerHour()
    {

    }

    private void ActionsPerDay()
    {

    }
}
