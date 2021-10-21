using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisingCampaign : MonoBehaviour, IAdvertisingCampaign
{
    [SerializeField] private float _monthlyCost = 1f;
    [SerializeField] private float _spawnRateIncrease = 1.05f;
    [SerializeField] private float _admissionFeeRebate = 1f;

    public Park Park { get; set; }
    public float MonthlyCost
    {
        get { return _monthlyCost; }
        set { _monthlyCost = Mathf.Max(0f, value); }
    }
    public float SpawnRateIncrease
    {
        get { return _spawnRateIncrease; }
        set { _spawnRateIncrease = Mathf.Max(0f, value); }
    }
    public float AdmissionFeeRebate
    {
        get { return _admissionFeeRebate; }
        set { _admissionFeeRebate = Mathf.Max(0f, value); }
    }

    public void OnNewMonth(object sender, System.EventArgs e)
    {
        if (Park.Bankroll >= MonthlyCost)
        {
            Park.SpendMoney(MonthlyCost);
        }
        else
        {
            Park.StopAdCampaign(this);
        }
    }
}
