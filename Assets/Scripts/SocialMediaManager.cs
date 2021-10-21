using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaManager : MonoBehaviour
{
    public AdvertisingCampaign[] _possibleAdCampaigns;
    public Park Park { get; set; }
    public float MonthlySalary { get; set; }

    public void OnNewYear(object sender, System.EventArgs e)
    {
        StartNewAdCampaign();
    }

    private void StartNewAdCampaign()
    {
        int campaignIndex = (int)UnityEngine.Random.Range(0, _possibleAdCampaigns.Length - 1);
        AdvertisingCampaign newCampaign = Instantiate(_possibleAdCampaigns[campaignIndex]);
        if (Park.Bankroll >= newCampaign.MonthlyCost)
        {
            Park.StartAdCampaign(newCampaign);
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
