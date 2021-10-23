using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IUpdatesDaily
{
    [SerializeField] private string _shopName;
    [SerializeField] private float _profitPerItem = 1f;
    [SerializeField] private float _shopCost = 0f;

    public Park Park { get; set; }
    public float ProfitPerItem => _profitPerItem;
    public float ShopCost => _shopCost;

    public void OnNewDay(object sender, EventArgs e)
    {
        float dailyProfit = Park.GuestsCount * _profitPerItem;
        Park.AddToBankroll(dailyProfit);
    }

    public override string ToString()
    {
        return _shopName + "; profit of $" + ProfitPerItem + " per item sold";
    }
}
