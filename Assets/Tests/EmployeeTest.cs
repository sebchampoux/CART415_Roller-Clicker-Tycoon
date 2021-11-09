using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EmployeeTest
{
    public class ConcreteEmployee : Employee
    {
        public override void OnNewYear(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    private MockTimer _timer;
    private MockPark _park;
    private Employee _employee;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<ConcreteEmployee>();
        _employee = tempGameObject.GetComponent<ConcreteEmployee>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _employee.Park = _park;
    }

    [Test]
    public void shouldHaveMontlyWageProp()
    {
        Assert.GreaterOrEqual(_employee.MonthlySalary, 0f);
    }

    [Test]
    public void shouldBePaidFromParkBankrollMonthly()
    {
        _park.AddToBankroll(500f);
        const float monthlySalary = 100f;
        _employee.MonthlySalary = monthlySalary;
        _timer.OnNewMonth += _employee.OnNewMonth;

        Assert.IsFalse(_park.SpendMoneyLastCalledWith(monthlySalary));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.SpendMoneyLastCalledWith(monthlySalary));
    }

    [Test]
    public void shouldBeFurloughedIfParkCantPaySalary()
    {
        const float monthlySalary = 100f;
        _employee.MonthlySalary = monthlySalary;
        _park.AddToBankroll(50f);
        _timer.OnNewMonth += _employee.OnNewMonth;

        Assert.IsFalse(_park.LastFurloughedEmployeeWas(_employee));
        _timer.ElapseMonth();
        Assert.IsTrue(_park.LastFurloughedEmployeeWas(_employee));
    }
}
