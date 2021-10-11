using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    [SerializeField] private float _admissionFee = 0f;
 
    private int _guestsCount = 0;
    private float _bankroll = 0f;
    private IList<AdvertisingCampaign> _runningCampaigns = new List<AdvertisingCampaign>();

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

    public float AdmissionFee
    {
        get { return _admissionFee; }
        private set { _admissionFee = Mathf.Max(0, value); }
    }

    public virtual void SpawnGuests(int numberOfGuests = 1)
    {
        GuestsCount += numberOfGuests;
        Bankroll += AdmissionFee * numberOfGuests;
    }

    public void SetBaseAdmissionFee(float newBaseAdmissionFee)
    {
        AdmissionFee = newBaseAdmissionFee;
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

    public void StartAdCampaign(AdvertisingCampaign campaign)
    {
        _runningCampaigns.Add(campaign);
    }

    public virtual void StopAdCampaign(AdvertisingCampaign campaign)
    {
        _runningCampaigns.Remove(campaign);
    }
}
