using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    [SerializeField] private float _baseAdmissionFee = 10f;
    [SerializeField] private Timer _timer;

    private float _admissionFee = 10f;
    private float _computedSpawnRate = 1f;
    private int _guestsCount = 0;
    private float _bankroll = 0f;
    private IList<AdvertisingCampaign> _runningCampaigns = new List<AdvertisingCampaign>();
    private IList<SocialMediaManager> _employees = new List<SocialMediaManager>();
    private IList<Ride> _rides = new List<Ride>();
    private IList<Shop> _shops = new List<Shop>();

    public event EventHandler OnGuestsCountChange;
    public event EventHandler OnBankrollChange;
    public event EventHandler OnParkOperationsChange; // new ride, shop, campaign, employee

    public IEnumerable<AdvertisingCampaign> AdvertisingCampaigns
    {
        get { return _runningCampaigns; }
    }
    public IEnumerable<SocialMediaManager> Employees
    {
        get { return _employees; }
    }

    public IEnumerable<Ride> Rides
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
        private set
        {
            _guestsCount = Mathf.Max(0, value);
            OnGuestsCountChange?.Invoke(this, null);
        }
    }

    public float Bankroll
    {
        get { return _bankroll; }
        private set
        {
            _bankroll = Mathf.Max(0, value);
            OnBankrollChange?.Invoke(this, null);
        }
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
        foreach (AdvertisingCampaign campaign in _runningCampaigns)
        {
            spawnRate *= campaign.SpawnRateIncrease;
        }
        _computedSpawnRate = spawnRate;
    }

    private void ComputeAdmissionFee()
    {
        float contributionFromRides = 0f;
        float rebateFromAds = 0f;
        foreach (Ride ride in _rides)
        {
            contributionFromRides += ride.ContributionToAdmissionFee;
        }
        foreach (AdvertisingCampaign campaign in _runningCampaigns)
        {
            rebateFromAds += campaign.AdmissionFeeRebate;
        }
        _admissionFee = Mathf.Max(0f, BaseAdmissionFee + contributionFromRides - rebateFromAds);
    }

    public virtual void SpawnGuests(int numberOfGuests = 1)
    {
        GuestsCount += Mathf.FloorToInt((float)numberOfGuests * SpawnRate);
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

    public virtual void StartAdCampaign(AdvertisingCampaign campaignPrefab)
    {
        if (Bankroll < campaignPrefab.MonthlyCost)
        {
            return;
        }
        AdvertisingCampaign campaign = Instantiate(campaignPrefab);
        campaign.transform.parent = transform;
        campaign.Park = this;
        _runningCampaigns.Add(campaign);
        _timer.OnNewMonth += campaign.OnNewMonth;

        ComputeAdmissionFee();
        ComputeSpawnRate();
        OnParkOperationsChange?.Invoke(this, null);
    }

    public virtual void StopAdCampaign(AdvertisingCampaign campaign)
    {
        _timer.OnNewMonth -= campaign.OnNewMonth;
        _runningCampaigns.Remove(campaign);
        Destroy(campaign);
        ComputeAdmissionFee();
        ComputeSpawnRate();
        OnParkOperationsChange?.Invoke(this, null);
    }

    public void HireEmployee(SocialMediaManager employeePrefab)
    {
        SocialMediaManager employee = Instantiate(employeePrefab);
        _timer.OnNewMonth += employee.OnNewMonth;
        _timer.OnNewYear += employee.OnNewYear;
        employee.transform.parent = transform;
        employee.Park = this;
        _employees.Add(employee);
        OnParkOperationsChange?.Invoke(this, null);
    }

    public virtual void FurloughEmployee(SocialMediaManager employee)
    {
        _timer.OnNewYear -= employee.OnNewYear;
        _timer.OnNewMonth -= employee.OnNewMonth;
        _employees.Remove(employee);
        Destroy(employee);
        OnParkOperationsChange?.Invoke(this, null);
    }

    public void AddNewRide(Ride ridePrefab)
    {
        if (Bankroll < ridePrefab.RideCost)
        {
            return;
        }
        SpendMoney(ridePrefab.RideCost);

        Ride newRide = Instantiate(ridePrefab);
        newRide.Park = this;
        newRide.transform.parent = transform;
        _timer.OnNewDay += newRide.OnNewDay;
        _rides.Add(newRide);
        
        ComputeAdmissionFee();
        OnParkOperationsChange?.Invoke(this, null);
    }

    public void AddNewShop(Shop shopPrefab)
    {
        if (Bankroll < shopPrefab.ShopCost)
        {
            return;
        }
        Shop shop = Instantiate(shopPrefab);
        shop.Park = this;
        shop.transform.parent = transform;
        _timer.OnNewDay += shop.OnNewDay;
        _shops.Add(shopPrefab);
        OnParkOperationsChange?.Invoke(this, null);
    }
}
