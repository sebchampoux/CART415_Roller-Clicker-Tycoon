using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour, IUpdatesDaily
{
    [SerializeField] private float _contributionToAdmissionFee = -1f;
    [SerializeField] private int _numberOfGuestsToSpawn = 1;
    [SerializeField] private float _rideCost = 100f;

    public float ContributionToAdmissionFee
    {
        get { return _contributionToAdmissionFee; }
        set { _contributionToAdmissionFee = Mathf.Max(0f, value); }
    }

    public int NumberOfGuestsToSpawn => _numberOfGuestsToSpawn;
    public float RideCost => _rideCost;
    public Park Park { get; set; }

    public void OnNewDay(object sender, System.EventArgs e)
    {
        Park.SpawnGuests(NumberOfGuestsToSpawn);
    }
}
