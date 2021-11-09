using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkPlanner : Employee
{
    public Ride[] PossibleRidesPrefabs { get; set; }

    public override void OnNewYear(object sender, EventArgs e)
    {
        int ridePrefabIndex = (int)UnityEngine.Random.Range(0, PossibleRidesPrefabs.Length - 1);
        Ride prefab = PossibleRidesPrefabs[ridePrefabIndex];
        Park.AddNewRide(prefab);
    }
}
