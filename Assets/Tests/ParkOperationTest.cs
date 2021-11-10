using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParkOperationTest
{
    public class ConcreteParkOperation : ParkOperation
    {
        private bool _wasTerminated = false;
        public bool Terminated => _wasTerminated;
        public override void Terminate()
        {
            _wasTerminated = true;
        }
    }

    private MockTimer _timer;
    private MockPark _park;
    private ConcreteParkOperation _parkOperation;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<ConcreteParkOperation>();
        _parkOperation = tempGameObject.GetComponent<ConcreteParkOperation>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _parkOperation.Park = _park;

        _timer.OnNewMonth += _parkOperation.OnNewMonth;
    }

    [Test]
    public void shouldHaveMonthlyCostProp()
    {
        Assert.GreaterOrEqual(_parkOperation.MonthlyCost, 0f);
    }

    [Test]
    public void shouldBePaidFromParksBankrollMonthly()
    {
        float campaignMonthlyCost = 100f;
        _park.AddToBankroll(500f);
        _parkOperation.MonthlyCost = campaignMonthlyCost;

        Assert.IsTrue(_park.SpendMoneyLastCalledWith(-1f));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.SpendMoneyLastCalledWith(campaignMonthlyCost));
    }

    [Test]
    public void shouldStopIfParkBankrollInsufficient()
    {
        // Too expensive
        _park.AddToBankroll(50f);
        _parkOperation.MonthlyCost = 100f;

        Assert.IsFalse(_parkOperation.Terminated);
        _timer.ElapseMonth();
        Assert.IsTrue(_parkOperation.Terminated);
    }
}
