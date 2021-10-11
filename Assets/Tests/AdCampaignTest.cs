using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AdCampaignTest
{
    private MockTimer _timer;
    private MockPark _park;
    private AdvertisingCampaign _adCampaign;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<AdvertisingCampaign>();
        _adCampaign = tempGameObject.GetComponent<AdvertisingCampaign>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _adCampaign.Park = _park;

        _timer.OnNewMonth += _adCampaign.OnNewMonth;
    }

    [Test]
    public void shouldHaveMonthlyCostProp()
    {
        Assert.GreaterOrEqual(_adCampaign.MonthlyCost, 0f);
    }

    [Test]
    public void shouldHaveSpawnRateIncreaseProp()
    {
        Assert.GreaterOrEqual(_adCampaign.SpawnRateIncrease, 0f);
    }

    [Test]
    public void shouldHaveAdmissionFeeRebateProp()
    {
        Assert.GreaterOrEqual(_adCampaign.AdmissionFeeRebate, 0f);
    }

    [Test]
    public void shouldBePaidFromParksBankrollMonthly()
    {
        float campaignMonthlyCost = 100f;
        _park.AddToBankroll(500f);
        _adCampaign.MonthlyCost = campaignMonthlyCost;

        Assert.IsTrue(_park.SpendMoneyLastCalledWith(-1f));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.SpendMoneyLastCalledWith(campaignMonthlyCost));
    }

    [Test]
    public void shouldStopIfParkBankrollInsufficient()
    {
        // Campaign too expensive
        _park.AddToBankroll(50f);
        _adCampaign.MonthlyCost = 100f;

        Assert.IsFalse(_park.StopCampaignLastCalledWith(_adCampaign));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.StopCampaignLastCalledWith(_adCampaign));
    }
}
