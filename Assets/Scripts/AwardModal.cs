using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardModal : Modal
{
    public AwardDisplay awardDisplayPrefab;

    protected override void SubscribeToParkEvents()
    {
        _park.OnNewAwardReceived += OnNewAwardReceived;
    }

    private void OnNewAwardReceived(object sender, System.EventArgs e)
    {
        Award newAward = _park.LatestAward;
        AwardDisplay newAwardDisplay = Instantiate(awardDisplayPrefab);
        newAwardDisplay.award = newAward;
        newAwardDisplay.transform.SetParent(_scrollViewContent.transform);
        newAwardDisplay.transform.localScale = Vector3.one;
    }
}
