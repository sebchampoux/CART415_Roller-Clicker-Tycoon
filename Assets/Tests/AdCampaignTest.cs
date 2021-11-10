using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AdCampaignTest
{
    private MockPark _park;
    private AdvertisingCampaign _adCampaign;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<AdvertisingCampaign>();
        _adCampaign = tempGameObject.GetComponent<AdvertisingCampaign>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _adCampaign.Park = _park;
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
    public void shouldStopWhenTerminateIsCalled()
    {
        Assert.IsFalse(_park.StopCampaignLastCalledWith(_adCampaign));
        _adCampaign.Terminate();
        Assert.IsTrue(_park.StopCampaignLastCalledWith(_adCampaign));
    }
}
