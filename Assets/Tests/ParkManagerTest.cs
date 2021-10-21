using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParkManagerTest
{
    private ParkManager _parkManager;
    private MockPark _parkMock;
    private MockTimer _mockTimer;

    [SetUp]
    public void prepareBuilderTests()
    {
        GameObject temp = new GameObject();
        temp.AddComponent<ParkManager>();

        temp.AddComponent<MockPark>();
        temp.AddComponent<MockTimer>();

        _parkManager = temp.GetComponent<ParkManager>();
        _parkManager.Park = temp.GetComponent<MockPark>();
        _parkManager.Timer = temp.GetComponent<MockTimer>();
    }
}
