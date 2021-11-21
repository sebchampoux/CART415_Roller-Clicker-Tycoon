using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public Park park;
    public Ride ridePrefab;
    public AudioSource toiletSoundFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            park.SpawnGuests(25);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            park.AddToBankroll(1000f);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            park.SpawnGuests(ridePrefab.GuestsToUnlock);
            park.AddToBankroll(ridePrefab.InitialCost);
            park.AddNewRide(ridePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            toiletSoundFX.Play();
        }
    }
}
