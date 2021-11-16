using UnityEngine;
using System;

public abstract class Employee : ParkOperation, IUpdatesYearly
{
    public override void Terminate()
    {
        Park.FurloughEmployee(this);
    }

    public abstract void OnNewYear(object sender, EventArgs e);
}