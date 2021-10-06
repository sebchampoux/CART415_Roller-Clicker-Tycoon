using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    private int _guestsCount = 0;
    private float _bankroll = 0f;
    [SerializeField] private float _admissionFee = 0f;

    public int GuestsCount
    {
        get { return _guestsCount; }
        private set { _guestsCount = Mathf.Max(0, value); }
    }

    public float Bankroll
    {
        get { return _bankroll; }
        private set { _bankroll = Mathf.Max(0, value); }
    }

    public float AdmissionFee
    {
        get { return _admissionFee; }
        private set { _admissionFee = Mathf.Max(0, value); }
    }

    public virtual void SpawnGuests(int numberOfGuests = 1)
    {
        GuestsCount += numberOfGuests;
        Bankroll += AdmissionFee * numberOfGuests;
    }

    public void SetBaseAdmissionFee(float newBaseAdmissionFee)
    {
        AdmissionFee = newBaseAdmissionFee;
    }
}
