using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisingCampaign : MonoBehaviour, IUpdatesMonthly
{
    [SerializeField] private string _campaignName;
    [SerializeField] private float _monthlyCost = 0f;
    [SerializeField] private float _spawnRateIncrease = 1f;
    [SerializeField] private float _admissionFeeRebate = 0f;

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

    public override string ToString()
    {
        return _campaignName + "; $" + MonthlyCost + "/month, " + SpawnRateIncrease + "x spawn rate increase, rebate of $" + AdmissionFeeRebate + " on admission fee.";
    }
}
