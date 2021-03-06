using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class GUIManager : MonoBehaviour
{
    public Park _park;
    public Timer _timer;
    public TextMeshProUGUI _guestsCountText;
    public TextMeshProUGUI _moneyText;
    public TextMeshProUGUI _dateText;
    public Text _ticketPriceText;
    public Text _monthlyParkOperationCostText;

    private void Start()
    {
        _timer.OnNewDay += UpdateDateIndicator;
        _park.OnGuestsCountChange += UpdateGuestsIndicator;
        _park.OnBankrollChange += UpdateBankrollIndicator;
        _park.OnParkOperationsChange += UpdateOperationsIndicators;

        UpdateDateIndicator(this, null);
        UpdateGuestsIndicator(this, null);
        UpdateBankrollIndicator(this, null);
        UpdateOperationsIndicators(this, null);
    }

    private void UpdateOperationsIndicators(object sender, EventArgs e)
    {
        _ticketPriceText.text = "Tickets price: " + _park.AdmissionFee.ToString("C");
        _monthlyParkOperationCostText.text = "Monthly expenses: " + _park.ParkMonthlyRunningCost.ToString("C");
    }

    private void UpdateBankrollIndicator(object sender, EventArgs e)
    {
        _moneyText.text = _park.Bankroll.ToString("C");
    }

    private void UpdateGuestsIndicator(object sender, EventArgs e)
    {
        _guestsCountText.text = _park.GuestsCount.ToString();
    }

    private void UpdateDateIndicator(object sender, EventArgs e)
    {
        _dateText.text = _timer.ToString();
    }
}
