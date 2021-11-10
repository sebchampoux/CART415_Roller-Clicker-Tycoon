using UnityEngine;
using System;

public abstract class Employee : ParkOperation, IUpdatesYearly, IUnlockable
{
    [SerializeField] private int _guestsToUnlock;
    public int GuestsToUnlock => _guestsToUnlock;

    public override void Terminate()
    {
        Park.FurloughEmployee(this);
    }

    public abstract void OnNewYear(object sender, EventArgs e);
}