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
    public void shouldStartNewAdCampaignEachYear()
    {
        GameObject temp = new GameObject();
        temp.AddComponent<AdvertisingCampaign>();
        AdvertisingCampaign campaign = temp.GetComponent<AdvertisingCampaign>();
        campaign.MonthlyCost = 100f;
        _smManager.PossibleCampaignsPrefabs = new AdvertisingCampaign[] { campaign };

        // Park bankroll must be sufficient to start campaign
        _park.AddToBankroll(1000f);
        
        _timer.OnNewYear += _smManager.OnNewYear;
        Assert.IsFalse(_park.StartAdCampaignWasCalled());
        _timer.ElapseYear();
        Assert.IsTrue(_park.StartAdCampaignWasCalled());
    }
}
