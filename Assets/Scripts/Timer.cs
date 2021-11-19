using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _dayDurationInSeconds = 10.0f;
    [SerializeField] private int _daysPerMonth = 30;
    [SerializeField] private string[] _months = new string[] {
        "May",
        "June",
        "July",
        "August",
        "September",
        "October"
    };
    
    private bool _isRunning = true;
    private int _currentDay = 1;

    public event EventHandler OnNewDay;
    public event EventHandler OnNewMonth;
    public event EventHandler OnNewYear;
    public float DayDurationSeconds
    {
        get =>_dayDurationInSeconds;
        set { _dayDurationInSeconds = Mathf.Max(0f, value); }
    }
    
    public int DaysPerMonth
    {
        get => _daysPerMonth;
    }
    
    public int MonthsPerYear
    {
        get => _months.Length;
    }

    public string[] Months => _months;
    
    public int Year => _currentDay / (DaysPerMonth * MonthsPerYear) + 1;
    public string Month
    {
        get
        {
            int monthIndex = (_currentDay / _daysPerMonth) % MonthsPerYear;
            return _months[monthIndex];
        }
    }
    public int Day => _currentDay % _daysPerMonth;

    public virtual void Start()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (_isRunning)
        {
            yield return new WaitForSeconds(DayDurationSeconds);

            _currentDay++;
            InvokeNewDay();

            bool isNewMonth = _currentDay % DaysPerMonth == 0;
            bool isNewYear = _currentDay % (DaysPerMonth * MonthsPerYear) == 0;
            if (isNewMonth)
            {
                InvokeNewMonth();
            }
            if (isNewYear)
            {
                InvokeNewYear();
            }
        }
    }

    protected void InvokeNewYear()
    {
        OnNewYear?.Invoke(this, null);
    }

    protected void InvokeNewMonth()
    {
        OnNewMonth?.Invoke(this, null);
    }

    protected void InvokeNewDay()
    {
        OnNewDay?.Invoke(this, null);
    }

    // Don't call the methods below directly during the game.
    // Mostly for testing purposes.
    public void ElapseDay()
    {
        _currentDay++;
        InvokeNewDay();
    }

    public void ElapseMonth()
    {
        _currentDay += DaysPerMonth;
        InvokeNewMonth();
    }

    public void ElapseYear()
    {
        _currentDay += DaysPerMonth * MonthsPerYear;
        InvokeNewYear();
    }

    public override string ToString()
    {
        return Month + " " + Day + ", Year " + Year;
    }
}
