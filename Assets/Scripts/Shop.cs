using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : ParkOperation, IUpdatesDaily, IUnlockable
{
    [SerializeField] private float _profitPerItem = 1f;
    [SerializeField] private float _shopCost = 0f;
    [SerializeField] private int _guestsToUnlock = 0;
    public float ProfitPerItem => _profitPerItem;
    public float ShopCost => _shopCost;
    public int GuestsToUnlock => _guestsToUnlock;

    public override string GetDescription()
    {
        return "Cost: $" + _shopCost.ToString("C") + "\n"
            + "Monthly operation cost: $" + MonthlyCost.ToString("C") + "\n"
            + "Makes each guest spend $" + _profitPerItem.ToString("C") + " per day\n";
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
