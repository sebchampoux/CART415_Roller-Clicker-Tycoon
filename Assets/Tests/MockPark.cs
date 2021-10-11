using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPark : Park
{
    private int _lastSpawnNbrOfGuests = -1;
    private float _lastCallToAddToBankroll = -1f;
    private float _lastCallToSpendMoney = -1f;
    private AdvertisingCampaign _lastTerminatedCampaign = null;

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

    public override bool SpendMoney(float amountToSpend)
    {
        _lastCallToSpendMoney = amountToSpend;
        return base.SpendMoney(amountToSpend);
    }

    public bool SpendMoneyLastCalledWith(float expectedSpending)
    {
        return expectedSpending == _lastCallToSpendMoney;
    }

    public bool StopCampaignLastCalledWith(AdvertisingCampaign adCampaign)
    {
        return adCampaign == _lastTerminatedCampaign;
    }

    public override void StopAdCampaign(AdvertisingCampaign campaign)
    {
        base.StopAdCampaign(campaign);
        _lastTerminatedCampaign = campaign;
    }
}
