using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : ParkOperation, IUpdatesDaily
{
    [SerializeField] private float _contributionToAdmissionFee = 0f;
    [SerializeField] private int _numberOfGuestsToSpawn = 1;

    public float ContributionToAdmissionFee
    {
        get => _contributionToAdmissionFee;
        set { _contributionToAdmissionFee = Mathf.Max(0f, value); }
    }

    public int NumberOfGuestsToSpawn => _numberOfGuestsToSpawn;

    public override string GetDescription()
    {
        return "Cost: " + InitialCost.ToString("C") + "\n"
            + "Unlocks at " + GuestsToUnlock + " guests\n"
            + "Monthly operation cost: " + MonthlyCost.ToString("C") + "\n"
            + "Contribution to admission fee: + " + _contributionToAdmissionFee.ToString("C") + "\n"
            + "Spawns " + _numberOfGuestsToSpawn + " guests / day";
    }

    public void OnNewDay(object sender, System.EventArgs e)
    {
        Park.SpawnGuests(NumberOfGuestsToSpawn);
    }

    public override void Terminate()
    {
        Park.CloseRide(this);
    }
}
