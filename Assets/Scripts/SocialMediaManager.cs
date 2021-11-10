using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaManager : Employee
{
    public AdvertisingCampaign[] PossibleCampaignsPrefabs;

    public override void OnNewYear(object sender, System.EventArgs e)
    {
        StartNewAdCampaign();
    }

    private void StartNewAdCampaign()
    {
        int campaignIndex = (int)UnityEngine.Random.Range(0, PossibleCampaignsPrefabs.Length - 1);
        AdvertisingCampaign newCampaignPrefab = PossibleCampaignsPrefabs[campaignIndex];
        Park.StartAdCampaign(newCampaignPrefab);
    }

    public override string ToString()
    {
        return "Social Media Manager; starts a new campaign every year; monthly salary of $" + MonthlyCost;
    }
}
