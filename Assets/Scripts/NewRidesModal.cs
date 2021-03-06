using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRidesModal : NewParkItemModal
{
    public override void AddNewElementToPark(ParkOperation item)
    {
        Ride ridePrefab = item.GetComponent<Ride>();
        _park.AddNewRide(ridePrefab);
    }
}
