using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParkOperation : MonoBehaviour, IUpdatesMonthly
{
    [SerializeField] private float _monthlyCost = 0f;

    public Park Park { get; set; }
    public float MonthlyCost
    {
        get => _monthlyCost;
        set { _monthlyCost = Mathf.Max(0f, value); }
    }

    public void OnNewMonth(object sender, System.EventArgs e)
    {
        if (Park.Bankroll >= MonthlyCost)
        {
            Park.SpendMoney(MonthlyCost);
        }
        else
        {
            Terminate();
        }
    }

    public abstract void Terminate();
}