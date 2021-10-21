using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    [SerializeField] private float _baseAdmissionFee = 10f;

    private float _admissionFee = 10f;
    private float _computedSpawnRate = 1f;
    private int _guestsCount = 0;
    private float _bankroll = 0f;
    private IList<IAdvertisingCampaign> _runningCampaigns = new List<IAdvertisingCampaign>();
    private IList<SocialMediaManager> _employees = new List<SocialMediaManager>();
    private IList<IRide> _rides = new List<IRide>();
    private IList<Shop> _shops = new List<Shop>();

    public IEnumerable<IAdvertisingCampaign> AdvertisingCampaigns
    {
        get { return _runningCampaigns; }
    }
    public IEnumerable<SocialMediaManager> Employees
    {
        get { return _employees; }
    }

    public IEnumerable<IRide> Rides
    {
        get { return _rides; }
    }

    public IEnumerable<Shop> Shops
    {
        get { return _shops; }
    }

    public int GuestsCount
    {
        get { return _guestsCount; }
        private set { _guestsCount = Mathf.Max(0, value); }
    }

    public float Bankroll
    {
        get { return _bankroll; }
        private set { _bankroll = Mathf.Max(0, value); }
    }

    public float BaseAdmissionFee
    {
        get { return _baseAdmissionFee; }
        set
        {
            _baseAdmissionFee = Mathf.Max(0f, value);
            ComputeAdmissionFee();
        }
    }

    public float AdmissionFee
    {
        get { return _admissionFee; }
    }

    public float SpawnRate
    {
        get
        {
            return _computedSpawnRate;
        }
    }

    public void Start()
    {
        ComputeAdmissionFee();
        ComputeSpawnRate();
    }

    private void ComputeSpawnRate()
    {
        float spawnRate = 1f;
        foreach (IAdvertisingCampaign campaign in _runningCampaigns)
        {
            spawnRate *= campaign.SpawnRateIncrease;
        }
        _computedSpawnRate = spawnRate;
    }

    private void ComputeAdmissionFee()
    {
        float contributionFromRides = 0f;
        float rebateFromAds = 0f;
        foreach (IRide ride in _rides)
        {
            contributionFromRides += ride.ContributionToAdmissionFee;
        }
        foreach (IAdvertisingCampaign campaign in _runningCampaigns)
        {
            rebateFromAds += campaign.AdmissionFeeRebate;
        }
        _admissionFee = Mathf.Max(0f, BaseAdmissionFee + contributionFromRides - rebateFromAds);
    }

    public virtual void SpawnGuests(int numberOfGuests = 1)
    {
        GuestsCount += Mathf.FloorToInt((float) numberOfGuests * SpawnRate);
        Bankroll += AdmissionFee * numberOfGuests;
    }

    public virtual void AddToBankroll(float amountToAdd)
    {
        Bankroll += amountToAdd;
    }

    public virtual bool SpendMoney(float amountToSpend)
    {
        if (Bankroll < amountToSpend)
        {
            return false;
        }
        Bankroll -= amountToSpend;
        return true;
    }

    public virtual void StartAdCampaign(IAdvertisingCampaign campaign)
    {
        _runningCampaigns.Add(campaign);

        ComputeAdmissionFee();
        ComputeSpawnRate();
    }

    public virtual void StopAdCampaign(IAdvertisingCampaign campaign)
    {
        _runningCampaigns.Remove(campaign);
        ComputeAdmissionFee();
        ComputeSpawnRate();
    }

    public void HireEmployee(SocialMediaManager employee)
    {
        _employees.Add(employee);
    }

    public virtual void FurloughEmployee(SocialMediaManager employee)
    {
        _employees.Remove(employee);
    }

    public void AddNewRide(IRide ride)
    {
        _rides.Add(ride);
        ComputeAdmissionFee();
    }

    public void AddNewShop(Shop shop)
    {
        _shops.Add(shop);
    }
}
