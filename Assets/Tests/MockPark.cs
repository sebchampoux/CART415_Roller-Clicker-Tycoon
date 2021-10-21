using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPark : Park
{
    private int _lastSpawnNbrOfGuests = -1;
    private float _lastCallToAddToBankroll = -1f;
    private float _lastCallToSpendMoney = -1f;
    private IAdvertisingCampaign _lastTerminatedCampaign = null;
    private IAdvertisingCampaign _lastStartedCampaign = null;
    private SocialMediaManager _lastFurloughedEmployee = null;
    private bool _startAdCampaignWasCalled = false;

    public override void SpawnGuests(int numberOfGuests = 1)
    {
        base.SpawnGuests();
        _lastSpawnNbrOfGuests = numberOfGuests;
    }

    public bool SpawnLastCalledWith(int expectedNumberOfGuests)
    {
        return expectedNumberOfGuests == _lastSpawnNbrOfGuests;
    }

    public override void AddToBankroll(float amountToAdd)
    {
        base.AddToBankroll(amountToAdd);
        _lastCallToAddToBankroll = amountToAdd;
    }

    public bool AddBankrollLastCalledWith(float expectedProfit)
    {
        return expectedProfit == _lastCallToAddToBankroll;
    }

    public bool StartAdCampaignWasCalled()
    {
        return _startAdCampaignWasCalled;
    }

    public override bool SpendMoney(float amountToSpend)
    {
        _lastCallToSpendMoney = amountToSpend;
        return base.SpendMoney(amountToSpend);
    }

    public bool SpendMoneyLastCalledWith(float expectedSpending)
    {
        return expectedSpending == _lastCallToSpendMoney;
    }

    public bool StopCampaignLastCalledWith(IAdvertisingCampaign adCampaign)
    {
        return adCampaign == _lastTerminatedCampaign;
    }

    public override void StopAdCampaign(IAdvertisingCampaign campaign)
    {
        base.StopAdCampaign(campaign);
        _lastTerminatedCampaign = campaign;
    }

    public override void StartAdCampaign(IAdvertisingCampaign campaign)
    {
        base.StartAdCampaign(campaign);
        _lastStartedCampaign = campaign;
        _startAdCampaignWasCalled = true;
    }

    public bool LastStartedCampaignWas(IAdvertisingCampaign campaign)
    {
        return _lastStartedCampaign == campaign;
    }

    public override void FurloughEmployee(SocialMediaManager employee)
    {
        base.FurloughEmployee(employee);
        _lastFurloughedEmployee = employee;
    }

    public bool LastFurloughedEmployeeWas(SocialMediaManager employee)
    {
        return _lastFurloughedEmployee == employee;
    }
}
