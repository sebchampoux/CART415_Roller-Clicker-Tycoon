using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class GUIManager : MonoBehaviour
{
    public Park _park;
    public Timer _timer;
    public TextMeshProUGUI _guestsCountText;
    public TextMeshProUGUI _moneyText;
    public TextMeshProUGUI _dateText;

    private void Start()
    {
        _timer.OnNewDay += UpdateDateIndicator;
        _park.OnGuestsCountChange += UpdateGuestsIndicator;
        _park.OnBankrollChange += UpdateBankrollIndicator;

        UpdateDateIndicator(this, null);
        UpdateGuestsIndicator(this, null);
        UpdateBankrollIndicator(this, null);
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
