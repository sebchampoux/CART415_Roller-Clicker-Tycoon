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
        GameObject temp = new GameObject();

        temp.AddComponent<MockTimer>();
        _timer = temp.GetComponent<MockTimer>();

        temp.AddComponent<ParkPlanner>();
        _parkPlanner = temp.GetComponent<ParkPlanner>();

        temp.AddComponent<MockPark>();
        _park = temp.GetComponent<MockPark>();

        _parkPlanner.Park = _park;


        temp.AddComponent<Ride>();
        Ride ride = temp.GetComponent<Ride>();
        _parkPlanner.PossibleRidesPrefabs = new Ride[] { ride };
        _parkPlanner.Start();
    }

    [Test]
    public void shouldBuildNewRideEachYear()
    {
        _timer.OnNewYear += _parkPlanner.OnNewYear;
        Assert.IsFalse(_park.AddNewRideWasCalled());
        _timer.ElapseYear();
        Assert.IsTrue(_park.AddNewRideWasCalled());
    }
}
