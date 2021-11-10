using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : ParkOperation, IUpdatesDaily, IUnlockable
{
    [SerializeField] private int _guestsToUnlock = 0;
    [SerializeField] private string _rideType;
    [SerializeField] private float _contributionToAdmissionFee = 0f;
    [SerializeField] private int _numberOfGuestsToSpawn = 1;
    [SerializeField] private float _rideCost = 0f;

    public float ContributionToAdmissionFee
    {
        get => _contributionToAdmissionFee;
        set { _contributionToAdmissionFee = Mathf.Max(0f, value); }
    }
    public float RideCost
    {
        get => _rideCost;
        set { _rideCost = Mathf.Max(0f, value); }
    }
    public int NumberOfGuestsToSpawn => _numberOfGuestsToSpawn;
    public int GuestsToUnlock => _guestsToUnlock;

    public void OnNewDay(object sender, System.EventArgs e)
    {
        Park.SpawnGuests(NumberOfGuestsToSpawn);
    }

    public override void Terminate()
    {
        Park.CloseRide(this);
    }

    public override string ToString()
    {
        return _rideType + "; attracts " + NumberOfGuestsToSpawn + " guests per day; increases admission fee by $" + _contributionToAdmissionFee;
    }
}
