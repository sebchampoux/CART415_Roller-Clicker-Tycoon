using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IUpdatesDaily
{
    [SerializeField] private float _profitPerItem = 1f;

    public Park Park { get; set; }
    public float ProfitPerItem { get { return _profitPerItem; } }

    public void OnNewDay(object sender, EventArgs e)
    {
        float dailyProfit = Park.GuestsCount * _profitPerItem;
        Park.AddToBankroll(dailyProfit);
    }
}
