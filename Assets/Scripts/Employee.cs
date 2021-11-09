using UnityEngine;
using System;

public abstract class Employee : MonoBehaviour, IUpdatesYearly, IUpdatesMonthly, IUnlockable
{
    [SerializeField] private int _guestsToUnlock;
    [SerializeField] private float _monthlySalary;

    public Park Park { get; set; }
    public float MonthlySalary
    {
        get { return _monthlySalary; }
        set { _monthlySalary = Mathf.Max(0f, value); }
    }
    public int GuestsToUnlock => _guestsToUnlock;

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

    public abstract void OnNewYear(object sender, EventArgs e);
}