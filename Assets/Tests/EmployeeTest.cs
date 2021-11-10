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
    private MockPark _park;
    private Employee _employee;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<ConcreteEmployee>();
        _employee = tempGameObject.GetComponent<ConcreteEmployee>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _employee.Park = _park;
    }

    [Test]
    public void shouldBeFurloughedIfTerminated()
    {
        Assert.IsFalse(_park.LastFurloughedEmployeeWas(_employee));
        _employee.Terminate();
        Assert.IsTrue(_park.LastFurloughedEmployeeWas(_employee));
    }
}
