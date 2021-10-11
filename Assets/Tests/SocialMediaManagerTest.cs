using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SocialMediaManagerTest
{
    private MockTimer _timer;
    private MockPark _park;
    private SocialMediaManager _smManager;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<SocialMediaManager>();
        _smManager = tempGameObject.GetComponent<SocialMediaManager>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _smManager.Park = _park;
    }

    [Test]
    public void shouldHaveMontlyWageProp()
    {
        Assert.GreaterOrEqual(_smManager.MonthlySalary, 0f);
    }

    [Test]
    public void shouldStartNewAdCampaignEachYear()
    {
        _timer.OnNewYear += _smManager.OnNewYear;

        throw new System.NotImplementedException();
    }

    [Test]
    public void shouldBePaidFromParkBankrollMonthly()
    {
        const float monthlyWage = 100f;
        _smManager.MonthlySalary = monthlyWage;
        _timer.OnNewMonth += _smManager.OnNewMonth;

        Assert.IsFalse(_park.SpendMoneyLastCalledWith(monthlyWage));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.SpendMoneyLastCalledWith(monthlyWage));
    }

    [Test]
    public void shouldBeFurloughedIfParkCantPaySalary()
    {
        const float monthlySalary = 100f;
        _smManager.MonthlySalary = monthlySalary;
        _park.AddToBankroll(50f);
        _timer.OnNewMonth += _smManager.OnNewMonth;

        Assert.IsFalse(_park.LastFurloughedEmployeeWas(_smManager));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.LastFurloughedEmployeeWas(_smManager));
    }
}
