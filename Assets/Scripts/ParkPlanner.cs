using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkPlanner : Employee
{
    public Ride[] PossibleRidesPrefabs { get; set; }
    private ListOfUnlockables<Ride> possibleRidesList;

    public void Start()
    {
        possibleRidesList = new ListOfUnlockables<Ride>(PossibleRidesPrefabs, Park);
    }

    public override void OnNewYear(object sender, EventArgs e)
    {
        Ride prefab = possibleRidesList.GetARandomAvailableItem();
        Park.AddNewRide(prefab);
    }
}
