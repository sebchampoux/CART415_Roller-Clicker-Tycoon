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
    private AdvertisingCampaign _lastStartedCampaign = null;
    private Employee _lastFurloughedEmployee = null;
    private bool _startAdCampaignWasCalled = false;
    private bool _addNewRideWasCalled = false;

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

    public bool StopCampaignLastCalledWith(AdvertisingCampaign adCampaign)
    {
        return adCampaign == _lastTerminatedCampaign;
    }

    public override void StopAdCampaign(AdvertisingCampaign campaign)
    {
        _lastTerminatedCampaign = campaign;
    }

    public override void StartAdCampaign(AdvertisingCampaign campaign)
    {
        _lastStartedCampaign = campaign;
        _startAdCampaignWasCalled = true;
    }

    public bool LastStartedCampaignWas(AdvertisingCampaign campaign)
    {
        return _lastStartedCampaign == campaign;
    }

    public override void FurloughEmployee(Employee employee)
    {
        _lastFurloughedEmployee = employee;
    }

    public bool LastFurloughedEmployeeWas(Employee employee)
    {
        return _lastFurloughedEmployee == employee;
    }

    public override void AddNewRide(Ride ridePrefab)
    {
        _addNewRideWasCalled = true;
    }

    public bool AddNewRideWasCalled() => _addNewRideWasCalled;
}
