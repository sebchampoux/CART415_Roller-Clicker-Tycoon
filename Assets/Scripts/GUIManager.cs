using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GUIManager : MonoBehaviour
{
    public Park Park;
    public TextMeshProUGUI StatusText;

    private void Start()
    {
        Park.OnBankrollChange += OnParkChange;
        Park.OnGuestsCountChange += OnParkChange;
    }

    private void OnParkChange(object sender, System.EventArgs e)
    {
        RecomputeStatusText();
    }

    private void RecomputeStatusText()
    {
        String statusText = "";
        statusText += "Guests: " + Park.GuestsCount + "\n";
        statusText += "Bankroll: $" + Park.Bankroll;
        StatusText.text = statusText;
    }
}
