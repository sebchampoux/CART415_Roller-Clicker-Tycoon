using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TimerTest
{

    private Timer timer;
    private double numberOfCallsToNotify = 0;

    [SetUp]
    public void beforeTests()
    {
        GameObject timerGO = new GameObject();
        timerGO.AddComponent<Timer>();
        timer = timerGO.GetComponent<Timer>();
    }

    [Test]
    public void shouldHaveSubscribeMethod()
    {
        timer.OnNewDay += NotifyOnNewDay;
    }

    [UnityTest]
    public IEnumerator shouldCallSubscribersOnUpdate()
    {
        numberOfCallsToNotify = 0;
        timer.OnNewDay += NotifyOnNewDay;
        Assert.AreEqual(0, numberOfCallsToNotify);
        yield return new WaitForSeconds(timer.DayDurationSeconds);
        Assert.AreEqual(1, numberOfCallsToNotify);
        yield return new WaitForSeconds(timer.DayDurationSeconds);
        Assert.AreEqual(2, numberOfCallsToNotify);
    }

    public void NotifyOnNewDay(object sender, System.EventArgs e)
    {
        numberOfCallsToNotify++;
    }
}
