using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsManager : MonoBehaviour
{
    public Award[] Awards;
    public Park _park;
    private int _nextAward = 0;

    private class AwardsComparer : IComparer<Award>
    {
        public int Compare(Award x, Award y)
        {
            return x.GuestsToUnlock.CompareTo(y.GuestsToUnlock);
        }
    }

    public void Awake()
    {
        _park.OnGuestsCountChange += OnGuestsCountChange;
        Array.Sort(Awards, new AwardsComparer());
    }

    public void OnGuestsCountChange(object sender, EventArgs e)
    {
        if (_nextAward >= Awards.Length)
        {
            // All awards have been unlocked.
            _park.OnGuestsCountChange -= OnGuestsCountChange;
            Destroy(gameObject);
        }
        Award nextAward = Awards[_nextAward];
        if (_park.GuestsCount >= nextAward.GuestsToUnlock)
        {
            _park.AddAward(nextAward);
            _nextAward++;
        }
    }
}
