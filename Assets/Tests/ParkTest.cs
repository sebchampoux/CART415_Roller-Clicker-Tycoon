using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParkTest
{
    private Park _park;
    private MockTimer _mockTimer;

    [SetUp]
    public void setupTest()
    {
        GameObject tempGameObject = new GameObject();
        tempGameObject.AddComponent<Park>();
        _park = tempGameObject.GetComponent<Park>();

        tempGameObject.AddComponent<MockTimer>();
        _mockTimer = tempGameObject.GetComponent<MockTimer>();
    }

    [Test]
    public void shouldHaveGuestCountInitializedTo0()
    {
        Assert.AreEqual(0, _park.GuestsCount);
    }

    [Test]
    public void shouldSpawnGuestsWithDefaultSpawnRateOf1()
    {
        Assert.AreEqual(0, _park.GuestsCount);
        _park.SpawnGuests();
        Assert.AreEqual(1, _park.GuestsCount);
    }

    [Test]
    public void shouldHaveBankrollInitializedTo0()
    {
        Assert.AreEqual(0f, _park.Bankroll);
    }

    [Test]
    public void shouldBeAbleToSetAdmissionFee()
    {
        Assert.AreEqual(0, _park.AdmissionFee);
        _park.SetBaseAdmissionFee(40f);
        Assert.AreEqual(40f, _park.AdmissionFee);
    }

    [Test]
    public void shouldPerceiveAdmissionFeeFromNewGuests()
    {
        Assert.AreEqual(0, _park.GuestsCount);
        Assert.AreEqual(0, _park.Bankroll);

        _park.SetBaseAdmissionFee(40f);
        _park.SpawnGuests();

        Assert.AreEqual(1, _park.GuestsCount);
        Assert.AreEqual(40f, _park.Bankroll);
    }

    [Test]
    public void shouldSpawnAdditionalGuestsIfRequired()
    {
        Assert.AreEqual(0, _park.GuestsCount);
        Assert.AreEqual(0, _park.Bankroll);

        _park.SetBaseAdmissionFee(40f);
        _park.SpawnGuests(5);

        Assert.AreEqual(5, _park.GuestsCount);
        Assert.AreEqual(5f * 40f, _park.Bankroll);
    }

    [Test]
    public void shouldAddToBankrollCorrectly()
    {
        Assert.AreEqual(0f, _park.Bankroll);
        _park.AddToBankroll(100f);
        Assert.AreEqual(100f, _park.Bankroll);
        _park.AddToBankroll(150f);
        Assert.AreEqual(250f, _park.Bankroll);
    }

    [Test]
    public void shouldRemoveFromBankrollCorrectly()
    {
        _park.AddToBankroll(100f);
        Assert.AreEqual(100f, _park.Bankroll);

        Assert.IsTrue(_park.SpendMoney(60f));
        Assert.AreEqual(40f, _park.Bankroll);

        // Not enough money left, transaction fails
        Assert.IsFalse(_park.SpendMoney(60f));
        Assert.AreEqual(40f, _park.Bankroll);
    }

    [Test]
    public void shouldComputeAdmissionFeeCorrectly()
    {
        throw new UnityException("Not implemented");
    }

    [Test]
    public void shouldComputeSpawnRateCorrectly()
    {
        throw new UnityException("Not implemented");
    }

    [Test]
    public void shouldTerminateAdCampaignCorrectly()
    {
        throw new UnityException("Not implemented");
    }

    public void shouldHireEmployeeCorrectly()
    {
        throw new UnityException("Not implemented");
    }

    public void shouldFurloughEmployeeCorrectly()
    {
        throw new UnityException("Not implemented");
    }
}
