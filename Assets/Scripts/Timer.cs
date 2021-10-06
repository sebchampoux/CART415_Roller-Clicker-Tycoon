using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _dayDurationInSeconds = 1.0f;
    [SerializeField] private int _daysPerMonth = 30;
    [SerializeField] private int _monthsPerYear = 6;
    
    private bool _isRunning = true;
    private int _currentDay = 1;
    private int _currentMonth = 1;

    public event EventHandler OnNewDay;
    public event EventHandler OnNewMonth;
    public event EventHandler OnNewYear;
    public float DayDurationSeconds
    {
        get
        {
            return _dayDurationInSeconds;
        }
        set
        {
            _dayDurationInSeconds = Mathf.Max(0f, value);
        }
    }
    
    public int DaysPerMonth
    {
        get { return _daysPerMonth; }
    }
    
    public int MonthsPerYear
    {
        get { return _monthsPerYear; }
    }

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

            bool isNewMonth = _currentDay >= DaysPerMonth;
            bool isNewYear = _currentMonth >= MonthsPerYear;
            if (isNewMonth)
            {
                _currentMonth++;
                _currentDay = 0;
                InvokeNewMonth();
            }
            if (isNewYear)
            {
                _currentMonth = 0;
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
}
