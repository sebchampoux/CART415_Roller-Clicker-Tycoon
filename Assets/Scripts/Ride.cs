using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour, IRide
{
    [SerializeField] private float _contributionToAdmissionFee = -1f;
    [SerializeField] private int _numberOfGuestsToSpawn = 1;

    public float ContributionToAdmissionFee { get; }
    public int NumberOfGuestsToSpawn { get; }

    public Park Park { get; set; }

    public void OnNewDay(object sender, System.EventArgs e)
    {
        Park.SpawnGuests(NumberOfGuestsToSpawn);
    }
}