using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsManager : MonoBehaviour
{
    public Award[] Awards;
    public Park _park;
    private int _nextAward = 0;

    public void Start()
    {
        _park.OnGuestsCountChange += OnGuestsCountChange;
        Array.Sort(Awards, (a1, a2) => a2.GuestsToUnlock - a1.GuestsToUnlock);
    }

    public void OnGuestsCountChange(object sender, EventArgs e)
    {
        if (_nextAward >= Awards.Length)
        {
            // All awards have been unlocked.
            _park.OnGuestsCountChange -= OnGuestsCountChange;
            Destroy(gameObject);
        }
        if (_park.GuestsCount >= Awards[_nextAward].GuestsToUnlock)
        {
            _park.AddAward(Awards[_nextAward]);
            _nextAward++;
        }
    }
}
