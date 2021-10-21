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
        _park.BaseAdmissionFee = 40f;
        Assert.AreEqual(40f, _park.AdmissionFee);
    }

    [Test]
    public void shouldPerceiveAdmissionFeeFromNewGuests()
    {
        Assert.AreEqual(0, _park.GuestsCount);
        Assert.AreEqual(0, _park.Bankroll);

        _park.BaseAdmissionFee = 40f;
        _park.SpawnGuests();

        Assert.AreEqual(1, _park.GuestsCount);
        Assert.AreEqual(40f, _park.Bankroll);
    }

    [Test]
    public void shouldSpawnAdditionalGuestsIfRequired()
    {
        Assert.AreEqual(0, _park.GuestsCount);
        Assert.AreEqual(0, _park.Bankroll);

        _park.BaseAdmissionFee = 40f;
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
        // Admission fee computed as follow:
        // (Base admission fee) + (for each ride: contribution to admission fee) - (for each ad campaign: rebate)
        _park.BaseAdmissionFee = 10f;
        Assert.AreEqual(10f, _park.AdmissionFee);

        IRide ride1 = new MockRide(5f);
        IRide ride2 = new MockRide(10f);
        _park.AddNewRide(ride1);
        _park.AddNewRide(ride2);
        Assert.AreEqual(25f, _park.AdmissionFee);

        IAdvertisingCampaign a1 = new MockAdCampaign(5f, 0f);
        IAdvertisingCampaign a2 = new MockAdCampaign(2f, 0f);
        _park.StartAdCampaign(a1);
        _park.StartAdCampaign(a2);
        Assert.AreEqual(18f, _park.AdmissionFee);

        _park.StopAdCampaign(a2);
        Assert.AreEqual(20f, _park.AdmissionFee);
    }

    [Test]
    public void shouldComputeSpawnRateCorrectly()
    {
        // Spawn rate computed as follow
        // 1 * (for each ad campaign: spawn rate)
        // Number of guests generated by one call to spawn is:
        // floor(nbr_guests_to_spawn * spawn_rate)
        Assert.AreEqual(0, _park.GuestsCount);

        _park.SpawnGuests();
        Assert.AreEqual(1f, _park.SpawnRate);
        Assert.AreEqual(1, _park.GuestsCount);

        // Spawn rate will be 1.5 * 1.25 = 1.875
        MockAdCampaign campaign2 = new MockAdCampaign(0f, 1.25f);
        _park.StartAdCampaign(new MockAdCampaign(0f, 1.5f));
        _park.StartAdCampaign(campaign2);
        Assert.AreEqual(1.875f, _park.SpawnRate);

        _park.SpawnGuests(5); // 5 * 1.875 = 9.375 => 9 guests spawned
        Assert.AreEqual(10, _park.GuestsCount);

        _park.StopAdCampaign(campaign2);
        Assert.AreEqual(1.5f, _park.SpawnRate);
        _park.SpawnGuests(2);
        Assert.AreEqual(13, _park.GuestsCount);
    }

    [Test]
    public void shouldStartAdCampaignCorrectly()
    {
        AdvertisingCampaign campaign = new AdvertisingCampaign();

        IEnumerator<IAdvertisingCampaign> campaigns = _park.AdvertisingCampaigns.GetEnumerator();
        Assert.IsFalse(campaigns.MoveNext());

        _park.StartAdCampaign(campaign);
        IEnumerator<IAdvertisingCampaign> campaignsAfterAdd = _park.AdvertisingCampaigns.GetEnumerator();
        Assert.IsTrue(campaignsAfterAdd.MoveNext());
        Assert.AreEqual(campaign, campaignsAfterAdd.Current);
    }

    [Test]
    public void shouldTerminateAdCampaignCorrectly()
    {
        AdvertisingCampaign campaign = new AdvertisingCampaign();

        _park.StartAdCampaign(campaign);
        _park.StopAdCampaign(campaign);
        IEnumerator<IAdvertisingCampaign> campaignsAfterRemove = _park.AdvertisingCampaigns.GetEnumerator();
        Assert.IsFalse(campaignsAfterRemove.MoveNext());
    }

    [Test]
    public void shouldHireEmployeeCorrectly()
    {
        SocialMediaManager employee = new SocialMediaManager();

        IEnumerator<SocialMediaManager> employees = _park.Employees.GetEnumerator();
        Assert.IsFalse(employees.MoveNext());

        _park.HireEmployee(employee);
        IEnumerator<SocialMediaManager> employeesAfterAdd = _park.Employees.GetEnumerator();
        Assert.IsTrue(employeesAfterAdd.MoveNext());
        Assert.AreEqual(employee, employeesAfterAdd.Current);
    }

    [Test]
    public void shouldFurloughEmployeeCorrectly()
    {
        SocialMediaManager employee = new SocialMediaManager();

        _park.HireEmployee(employee);
        _park.FurloughEmployee(employee);
        IEnumerator<SocialMediaManager> employeesAfterRemove = _park.Employees.GetEnumerator();
        Assert.IsFalse(employeesAfterRemove.MoveNext());
    }

    [Test]
    public void shouldAddRideCorrectly()
    {
        Ride ride = new Ride();

        IEnumerator<IRide> rides = _park.Rides.GetEnumerator();
        Assert.IsFalse(rides.MoveNext());

        _park.AddNewRide(ride);
        IEnumerator<IRide> ridesAfterAdd = _park.Rides.GetEnumerator();
        Assert.IsTrue(ridesAfterAdd.MoveNext());
        Assert.AreEqual(ride, ridesAfterAdd.Current);
    }

    public class MockAdCampaign : IAdvertisingCampaign
    {

        public float _afr;
        public float _sri;

        public MockAdCampaign(float afr, float sri)
        {
            _afr = afr;
            _sri = sri;
        }

        public float AdmissionFeeRebate { get => _afr; }
        public float SpawnRateIncrease { get => _sri; }
    }

    public class MockRide : IRide
    {
        public float _ctaf;
        public int _ngs;

        public MockRide(float ctaf)
        {
            _ctaf = ctaf;
        }

        public float ContributionToAdmissionFee => _ctaf;
        public int NumberOfGuestsToSpawn => _ngs;
    }
}