using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GUIManager : MonoBehaviour
{
    public Park _park;
    public TextMeshProUGUI _statusText;
    private string _parkOperationsDisplay;

    private void Start()
    {
        _park.OnBankrollChange += OnGuestsBankrollChange;
        _park.OnGuestsCountChange += OnGuestsBankrollChange;
        _park.OnParkOperationsChange += OnParkOperationsChange;
        OnParkOperationsChange(this, null);
    }

    private void OnParkOperationsChange(object sender, EventArgs e)
    {
        ComputeParkOperationsText();
        ComputeParkDataText();
    }

    private void ComputeParkOperationsText()
    {
        _parkOperationsDisplay = "Rides:\n";
        foreach (Ride ride in _park.Rides)
        {
            _parkOperationsDisplay += ride + "\n";
        }
        _parkOperationsDisplay += "\nShops:\n";
        foreach (Shop shop in _park.Shops)
        {
            _parkOperationsDisplay += shop + "\n";
        }
        _parkOperationsDisplay += "\nAdvertising Campaigns:\n";
        foreach (AdvertisingCampaign campaign in _park.AdvertisingCampaigns)
        {
            _parkOperationsDisplay += campaign + "\n";
        }
        _parkOperationsDisplay += "\nEmployees:\n";
        foreach (Employee employee in _park.Employees)
        {
            _parkOperationsDisplay += employee + "\n";
        }
    }

    private void OnGuestsBankrollChange(object sender, System.EventArgs e)
    {
        ComputeParkDataText();
    }

    private void ComputeParkDataText()
    {
        string statusText = "";
        statusText += "Guests: " + _park.GuestsCount + "\n";
        statusText += "Bankroll: $" + _park.Bankroll + "\n";
        statusText += "\n" + _parkOperationsDisplay;
        _statusText.text = statusText;
    }
}
