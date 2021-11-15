using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisingCampaign : ParkOperation, IUnlockable
{
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

    public override string GetDescription()
    {
        return "Monthly operation cost: $" + MonthlyCost.ToString("C") + "\n"
            + "Increases spawn rate by " + _spawnRateIncrease + "x\n"
            + "Offers an entrance rebate of -$" + _admissionFeeRebate.ToString("C") + " to each new guest";
    }

    public override void Terminate()
    {
        Park.StopAdCampaign(this);
    }
}
