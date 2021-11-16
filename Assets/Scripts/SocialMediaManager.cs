using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaManager : Employee
{
    public AdvertisingCampaign[] PossibleCampaignsPrefabs;
    private ListOfUnlockables<AdvertisingCampaign> possibleCampaignList;

    public void Start()
    {
        possibleCampaignList = new ListOfUnlockables<AdvertisingCampaign>(PossibleCampaignsPrefabs, Park);
    }

    public override void OnNewYear(object sender, System.EventArgs e)
    {
        StartNewAdCampaign();
    }

    private void StartNewAdCampaign()
    {
        AdvertisingCampaign newCampaignPrefab = possibleCampaignList.GetARandomAvailableItem();
        Park.StartAdCampaign(newCampaignPrefab);
    }

    public override string GetDescription()
    {
        return "Monthly salary: " + MonthlyCost.ToString("C") + "\n"
            + "Starts a new ad campaign each year";
    }
}
