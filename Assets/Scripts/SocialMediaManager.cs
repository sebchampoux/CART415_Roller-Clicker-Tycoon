using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaManager : MonoBehaviour, IUpdatesYearly, IUpdatesMonthly
{
    public AdvertisingCampaign[] _campaignPrefabs;
    public float MonthlySalary;
    public Park Park { get; set; }
    
    public void OnNewYear(object sender, System.EventArgs e)
    {
        StartNewAdCampaign();
    }

    private void StartNewAdCampaign()
    {
        int campaignIndex = (int)UnityEngine.Random.Range(0, _campaignPrefabs.Length - 1);
        AdvertisingCampaign newCampaignPrefab = _campaignPrefabs[campaignIndex];
        if (Park.Bankroll >= newCampaignPrefab.MonthlyCost)
        {
            Park.StartAdCampaign(newCampaignPrefab);
        }
    }

    public void OnNewMonth(object sender, EventArgs e)
    {
        if (Park.Bankroll >= MonthlySalary)
        {
            Park.SpendMoney(MonthlySalary);
        }
        else
        {
            Park.FurloughEmployee(this);
        }
    }
}
