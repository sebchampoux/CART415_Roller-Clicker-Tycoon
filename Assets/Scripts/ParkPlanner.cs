using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkPlanner : Employee
{
    public Ride[] PossibleRidesPrefabs;
    private ListOfUnlockables<Ride> possibleRidesList;

    public override void Start()
    {
        possibleRidesList = new ListOfUnlockables<Ride>(PossibleRidesPrefabs, Park);
    }

    public override void OnNewYear(object sender, EventArgs e)
    {
        Ride prefab = possibleRidesList.GetARandomAvailableItem();
        Park.AddNewRide(prefab);
    }

    public override string GetDescription()
    {
        return "Monthly salary: " + MonthlyCost.ToString("C") + "\n"
            + "Unlocks at " + GuestsToUnlock + " guests\n"
            + "Builds a new ride every year";
    }
}
