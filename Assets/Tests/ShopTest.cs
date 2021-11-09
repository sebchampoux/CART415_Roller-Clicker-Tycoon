using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShopTest
{
    private MockTimer _timer;
    private MockPark _park;
    private Shop _shop;

    [SetUp]
    public void SetUpTests()
    {
        GameObject tempGameObject = new GameObject();

        tempGameObject.AddComponent<MockTimer>();
        _timer = tempGameObject.GetComponent<MockTimer>();

        tempGameObject.AddComponent<Shop>();
        _shop = tempGameObject.GetComponent<Shop>();

        tempGameObject.AddComponent<MockPark>();
        _park = tempGameObject.GetComponent<MockPark>();

        _shop.Park = _park;
    }

    [Test]
    public void shouldHaveProfitPerItemProp()
    {
        Assert.GreaterOrEqual(_shop.ProfitPerItem, 0f);
    }

    [Test]
    public void shouldMakeEachGuestSpendMoneyDaily()
    {
        Assert.AreEqual(0, _park.Bankroll);

        _timer.OnNewDay += _shop.OnNewDay;

        float expectedProfit = _park.GuestsCount * _shop.ProfitPerItem;
        _timer.ElapseDay();
        Assert.IsTrue(_park.AddBankrollLastCalledWith(expectedProfit));
    }

    [Test]
    public void shouldHaveMonthlyOperationCost()
    {
        throw new System.NotImplementedException();
    }

    [Test]
    public void shouldRemoveMonthlyOpCostFromParkBankroll()
    {
        throw new System.NotImplementedException();
    }

    [Test]
    public void shouldCloseIfCantPayMonthlyOpCost()
    {
        throw new System.NotImplementedException();
    }
}
