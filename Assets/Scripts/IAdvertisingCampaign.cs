using System;

public interface IAdvertisingCampaign : IUpdatesMonthly
{
    float AdmissionFeeRebate { get; }
    float SpawnRateIncrease { get; }
}