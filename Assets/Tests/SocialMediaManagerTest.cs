using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SocialMediaManagerTest
{
    private MockTimer _timer;
    private MockPark _park;
    private SocialMediaManager _smManager;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<SocialMediaManager>();
        _smManager = tempGameObject.GetComponent<SocialMediaManager>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _smManager.Park = _park;
    }

    [Test]
    public void shouldBePaidFromParkBankrollMonthly()
    {
        _park.AddToBankroll(500f);
        const float monthlySalary = 100f;
        _smManager.MonthlySalary = monthlySalary;
        _timer.OnNewMonth += _smManager.OnNewMonth;

        Assert.IsFalse(_park.SpendMoneyLastCalledWith(monthlySalary));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.SpendMoneyLastCalledWith(monthlySalary));
    }
}
