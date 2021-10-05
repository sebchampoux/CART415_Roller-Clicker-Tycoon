using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParkTest
{
    private Park _park; 

    [SetUp]
    public void setupTest()
    {
        GameObject parkGO = new GameObject();
        parkGO.AddComponent<Park>();
        _park = parkGO.GetComponent<Park>();
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
}
