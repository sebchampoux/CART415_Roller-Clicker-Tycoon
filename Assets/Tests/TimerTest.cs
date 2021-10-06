using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TimerTest
{

    private Timer _timer;
    private double _numberOfCallsToNotifyDay = 0;
    private bool _newMonthWasCalled = false;
    private bool _newYearWasCalled = false;

    [SetUp]
    public void beforeTests()
    {
        GameObject timerGO = new GameObject();
        timerGO.AddComponent<Timer>();
        _timer = timerGO.GetComponent<Timer>();
        _timer.DayDurationSeconds = 0.01f;
    }

    [UnityTest]
    public IEnumerator shouldNotifyOnNewDay()
    {
        _numberOfCallsToNotifyDay = 0;
        _timer.OnNewDay += NotifyOnNewDay;
        Assert.AreEqual(0, _numberOfCallsToNotifyDay);
        yield return new WaitForSeconds(_timer.DayDurationSeconds);
        Assert.AreEqual(1, _numberOfCallsToNotifyDay);
        yield return new WaitForSeconds(_timer.DayDurationSeconds);
        Assert.AreEqual(2, _numberOfCallsToNotifyDay);
    }

    [UnityTest]
    public IEnumerator shouldNotifyOnNewMonth()
    {
        Assert.IsFalse(_newMonthWasCalled);
        _timer.OnNewMonth += Timer_OnNewMonth;

        // Wait for a month to elapse
        for(int i = 0; i < _timer.DaysPerMonth; i++)
        {
            yield return new WaitForSeconds(_timer.DayDurationSeconds);
        }
        Assert.IsTrue(_newMonthWasCalled);
    }

    [UnityTest]
    public IEnumerator shouldNotifyOnNewYear()
    {
        Assert.IsFalse(_newYearWasCalled);
        _timer.OnNewYear += Timer_OnNewYear;

        // Wait for a year to elapse
        for(int j = 0; j < _timer.MonthsPerYear; j++)
        {
            for (int i = 0; i < _timer.DaysPerMonth; i++)
            {
                yield return new WaitForSeconds(_timer.DayDurationSeconds);
            }
        }
        Assert.IsTrue(_newYearWasCalled);
    }

    private void Timer_OnNewMonth(object sender, System.EventArgs e)
    {
        _newMonthWasCalled = true;
    }

    private void Timer_OnNewYear(object sender, System.EventArgs e)
    {
        _newYearWasCalled = true;
    }

    public void NotifyOnNewDay(object sender, System.EventArgs e)
    {
        _numberOfCallsToNotifyDay++;
    }
}
