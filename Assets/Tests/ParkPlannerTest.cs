using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParkPlannerTest
{
    private MockTimer _timer;
    private MockPark _park;
    private ParkPlanner _parkPlanner;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<ParkPlanner>();
        _parkPlanner = tempGameObject.GetComponent<ParkPlanner>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _parkPlanner.Park = _park;
    }

    [Test]
    public void shouldBuildNewRideEachYear()
    {
        GameObject temp = new GameObject();
        temp.AddComponent<Ride>();
        Ride ride = temp.GetComponent<Ride>();
        ride.RideCost = 0f;

        _parkPlanner.PossibleRidesPrefabs = new Ride[] { ride };

        _timer.OnNewYear += _parkPlanner.OnNewYear;
        Assert.IsFalse(_park.AddNewRideWasCalled());
        _timer.ElapseYear();
        Assert.IsTrue(_park.AddNewRideWasCalled());
    }
}
