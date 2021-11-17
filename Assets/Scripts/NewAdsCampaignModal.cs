using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAdsCampaignModal : Modal
{
    public override void AddNewElementToPark(ParkOperation item)
    {
        AdvertisingCampaign campaignPrefab = item.GetComponent<AdvertisingCampaign>();
        _park.StartAdCampaign(campaignPrefab);
    }
}
