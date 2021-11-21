using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    [SerializeField] private float _baseAdmissionFee = 10f;
    [SerializeField] private Timer _timer;
    [SerializeField] private Transform _ridesPanel;
    [SerializeField] private Transform _shopsPanel;
    [SerializeField] private Transform _adsPanel;
    [SerializeField] private Transform _staffPanel;

    private float _admissionFee;
    private float _computedSpawnRate = 1f;
    private int _guestsCount = 0;
    private float _bankroll = 0f;
    private IList<AdvertisingCampaign> _runningCampaigns = new List<AdvertisingCampaign>();
    private IList<Employee> _employees = new List<Employee>();
    private IList<Ride> _rides = new List<Ride>();
    private IList<Shop> _shops = new List<Shop>();
    private IList<Award> _awards = new List<Award>();

    public event EventHandler OnGuestsCountChange;
    public event EventHandler OnBankrollChange;
    public event EventHandler OnParkOperationsChange; // new ride, shop, campaign, employee
    public event EventHandler OnNewAwardReceived;

    public Timer Timer { set { _timer = value; } }
    public IEnumerable<AdvertisingCampaign> AdvertisingCampaigns
    {
        get { return _runningCampaigns; }
    }
    public IEnumerable<Employee> Employees
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
        protected set
        {
            _guestsCount = value;
            OnGuestsCountChange?.Invoke(this, null);
        }
    }

    public float Bankroll
    {
        get { return _bankroll; }
        protected set
        {
            _bankroll = value;
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

    public float ParkMonthlyRunningCost
    {
        get => ComputeMonthlyCost();
    }

    private float ComputeMonthlyCost()
    {
        float monthlyCost = 0f;
        foreach(Ride r in Rides)
        {
            monthlyCost += r.MonthlyCost;
        }
        foreach(Shop s in Shops)
        {
            monthlyCost += s.MonthlyCost;
        }
        foreach(AdvertisingCampaign a in AdvertisingCampaigns)
        {
            monthlyCost += a.MonthlyCost;
        }
        foreach(Employee e in Employees)
        {
            monthlyCost += e.MonthlyCost;
        }
        return monthlyCost;
    }

    public IEnumerable<Award> Awards => _awards;
    public Award LatestAward => _awards[_awards.Count - 1];

    public void Awake()
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
        _admissionFee = BaseAdmissionFee;
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
        campaign.transform.parent = _adsPanel;
        campaign.transform.localScale = Vector3.one;
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

    public void HireEmployee(Employee employeePrefab)
    {
        Employee employee = Instantiate(employeePrefab);
        _timer.OnNewMonth += employee.OnNewMonth;
        _timer.OnNewYear += employee.OnNewYear;
        employee.transform.parent = _staffPanel;
        employee.transform.localScale = Vector3.one;
        employee.Park = this;
        _employees.Add(employee);
        OnParkOperationsChange?.Invoke(this, null);
    }

    public virtual void FurloughEmployee(Employee employee)
    {
        _timer.OnNewYear -= employee.OnNewYear;
        _timer.OnNewMonth -= employee.OnNewMonth;
        _employees.Remove(employee);
        Destroy(employee);
        OnParkOperationsChange?.Invoke(this, null);
    }

    public virtual void AddNewRide(Ride ridePrefab)
    {
        if (Bankroll < ridePrefab.InitialCost)
        {
            return;
        }
        SpendMoney(ridePrefab.InitialCost);

        Ride newRide = Instantiate(ridePrefab);
        newRide.Park = this;
        newRide.transform.parent = _ridesPanel;
        newRide.transform.localScale = Vector3.one;
        _timer.OnNewDay += newRide.OnNewDay;
        _timer.OnNewMonth += newRide.OnNewMonth;
        _rides.Add(newRide);

        ComputeAdmissionFee();
        OnParkOperationsChange?.Invoke(this, null);
    }

    public virtual void CloseRide(Ride ride)
    {
        _timer.OnNewDay -= ride.OnNewDay;
        _timer.OnNewMonth -= ride.OnNewMonth;
        _rides.Remove(ride);
        Destroy(ride);
        ComputeAdmissionFee();
        OnParkOperationsChange?.Invoke(this, null);
    }

    public void AddNewShop(Shop shopPrefab)
    {
        if (Bankroll < shopPrefab.InitialCost)
        {
            return;
        }
        Shop shop = Instantiate(shopPrefab);
        shop.Park = this;
        shop.transform.parent = _shopsPanel;
        shop.transform.localScale = Vector3.one;
        _timer.OnNewDay += shop.OnNewDay;
        _timer.OnNewMonth += shop.OnNewMonth;
        _shops.Add(shop);
        OnParkOperationsChange?.Invoke(this, null);
    }

    public virtual void CloseShop(Shop shop)
    {
        _timer.OnNewDay -= shop.OnNewDay;
        _timer.OnNewMonth -= shop.OnNewMonth;
        _shops.Remove(shop);
        Destroy(shop);
        OnParkOperationsChange?.Invoke(this, null);
    }

    public virtual void AddAward(Award award)
    {
        _awards.Add(award);
        OnNewAwardReceived?.Invoke(this, null);
    }
}
