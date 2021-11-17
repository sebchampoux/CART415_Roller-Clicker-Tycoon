using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : ParkOperation, IUpdatesDaily
{
    [SerializeField] private float _profitPerItem = 1f;
    public float ProfitPerItem => _profitPerItem;

    public override string GetDescription()
    {
        return "Cost: " + InitialCost.ToString("C") + "\n"
            + "Unlocks at " + GuestsToUnlock + " guests\n"
            + "Monthly operation cost: " + MonthlyCost.ToString("C") + "\n"
            + "Makes each guest spend " + _profitPerItem.ToString("C") + " per day\n";
    }

    public void OnNewDay(object sender, EventArgs e)
    {
        float dailyProfit = Park.GuestsCount * _profitPerItem;
        Park.AddToBankroll(dailyProfit);
    }

    public override void Terminate()
    {
        Park.CloseShop(this);
    }
}
