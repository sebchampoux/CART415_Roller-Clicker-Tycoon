using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisingCampaign : ParkOperation, IUnlockable
{
    [SerializeField] private string _campaignName;
    [SerializeField] private float _spawnRateIncrease = 1f;
    [SerializeField] private float _admissionFeeRebate = 0f;
    [SerializeField] private int _guestsToUnlock = 0;

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

    public int GuestsToUnlock => _guestsToUnlock;

    public override void Terminate()
    {
        Park.StopAdCampaign(this);
    }

    public override string ToString()
    {
        return _campaignName + "; $" + MonthlyCost + "/month, " + SpawnRateIncrease + "x spawn rate increase, rebate of $" + AdmissionFeeRebate + " on admission fee.";
    }
}
