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
        GameObject temp = new GameObject();
        temp.AddComponent<AdvertisingCampaign>();
        AdvertisingCampaign campaign = temp.GetComponent<AdvertisingCampaign>();
        campaign.MonthlyCost = 100f;
        _smManager._campaignPrefabs = new AdvertisingCampaign[] { campaign };

        // Park bankroll must be sufficient to start campaign
        _park.AddToBankroll(1000f);
        
        _timer.OnNewYear += _smManager.OnNewYear;
        Assert.IsFalse(_park.StartAdCampaignWasCalled());
        _timer.ElapseYear();
        Assert.IsTrue(_park.StartAdCampaignWasCalled());
    }

    [Test]
    public void shouldBePaidFromParkBankrollMonthly()
    {
        _park.AddToBankroll(500f);
        const float monthlySalary = 100f;
        _smManager.MonthlySalary = monthlySalary;
        _timer.OnNewMonth += _smManager.OnNewMonth;

        Assert.IsFalse(_park.SpendMoneyLastCalledWith(monthlySalary));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.SpendMoneyLastCalledWith(monthlySalary));
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
