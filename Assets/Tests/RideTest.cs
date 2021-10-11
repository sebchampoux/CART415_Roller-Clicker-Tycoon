using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RideTest
{
    private MockTimer _timer;
    private MockPark _park;
    private Ride _ride;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<Ride>();
        _ride = tempGameObject.GetComponent<Ride>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _ride.Park = _park;
    }

    [Test]
    public void shouldHaveContributionToAdmissionFeeProp()
    {
        Assert.GreaterOrEqual(0f, _ride.ContributionToAdmissionFee);
    }

    [Test]
    public void shouldAutomaticallySpawnGuestsEachDay()
    {
        _timer.OnNewDay += _ride.OnNewDay;

        _timer.ElapseDay();
        Assert.IsTrue(_park.SpawnLastCalledWith(_ride.NumberOfGuestsToSpawn));
    }
}
