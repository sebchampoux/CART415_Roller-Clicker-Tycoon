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
        GameObject temp = new GameObject();

        temp.AddComponent<MockTimer>();
        _timer = temp.GetComponent<MockTimer>();

        temp.AddComponent<SocialMediaManager>();
        _smManager = temp.GetComponent<SocialMediaManager>();

        temp.AddComponent<MockPark>();
        _park = temp.GetComponent<MockPark>();
        _park.SpawnGuests(500);

        _smManager.Park = _park;

        temp.AddComponent<AdvertisingCampaign>();
        AdvertisingCampaign campaign = temp.GetComponent<AdvertisingCampaign>();
        _smManager.PossibleCampaignsPrefabs = new AdvertisingCampaign[] { campaign };
        _smManager.Start();
    }

    [Test]
    public void shouldStartNewAdCampaignEachYear()
    {
        _timer.OnNewYear += _smManager.OnNewYear;
        Assert.IsFalse(_park.StartAdCampaignWasCalled());
        _timer.ElapseYear();
        Assert.IsTrue(_park.StartAdCampaignWasCalled());
    }
}
