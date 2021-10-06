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
        _ride.Timer = _timer;
    }

    [Test]
    public void shouldHaveContributionToAdmissionFeeProp()
    {
        Assert.GreaterOrEqual(0f, _ride.ContributionToAdmissionFee);
    }

    [Test]
    public void shouldAutomaticallySpawnGuestsEachDay()
    {
        Assert.AreEqual(0, _park.NumberOfCallsToSpawn);

        _timer.OnNewDay += _ride.OnNewDay;

        _timer.ElapseDay();
        Assert.AreEqual(1, _park.NumberOfCallsToSpawn);
        Assert.IsTrue(_park.SpawnLastCalledWith(_ride.NumberOfGuestsToSpawn));

        _timer.ElapseDay();
        Assert.AreEqual(2, _park.NumberOfCallsToSpawn);
        Assert.IsTrue(_park.SpawnLastCalledWith(_ride.NumberOfGuestsToSpawn));
    }
}
