using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPark : Park
{
    private int _lastSpawnNbrOfGuests = -1;
    private float _lastCallToAddToBankroll = -1f;
    public int NumberOfCallsToSpawn { get; private set; } = 0;

    public override void SpawnGuests(int numberOfGuests = 1)
    {
        NumberOfCallsToSpawn++;
        _lastSpawnNbrOfGuests = numberOfGuests;
    }

    public bool SpawnLastCalledWith(int expectedNumberOfGuests)
    {
        return NumberOfCallsToSpawn > 0 && expectedNumberOfGuests == _lastSpawnNbrOfGuests;
    }

    public override void AddToBankroll(float amountToAdd)
    {
        _lastCallToAddToBankroll = amountToAdd;
    }

    public bool AddBankrollLastCalledWith(float expectedProfit)
    {
        return expectedProfit == _lastCallToAddToBankroll;
    }

    public void ResetMock()
    {
        _lastSpawnNbrOfGuests = -1;
        _lastCallToAddToBankroll = -1;
    }
}
