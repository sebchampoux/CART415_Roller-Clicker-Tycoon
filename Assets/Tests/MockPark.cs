using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPark : Park
{
    private int _lastSpawnNbrOfGuests = -1;
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
}
