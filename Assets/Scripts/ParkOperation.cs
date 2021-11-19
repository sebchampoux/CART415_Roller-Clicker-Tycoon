using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ParkOperation : MonoBehaviour, IUnlockable, IUpdatesMonthly
{
    [SerializeField] private float _monthlyCost = 0f;
    [SerializeField] private int _guestsToUnlock = 0;
    [SerializeField] private float _initialCost = 0f;
    public string Name;
    public Sprite Icon;
    public Park Park;
    private Image _imageComponent;

    public float MonthlyCost
    {
        get => _monthlyCost;
        set { _monthlyCost = Mathf.Max(0f, value); }
    }

    public int GuestsToUnlock => _guestsToUnlock;
    public float InitialCost
    {
        get => _initialCost;
        set { _initialCost = Mathf.Max(0f, value); }
    }

    public void OnNewMonth(object sender, System.EventArgs e)
    {
        if (Park.Bankroll >= MonthlyCost)
        {
            Park.SpendMoney(MonthlyCost);
        }
        else
        {
            Terminate();
        }
    }

    public abstract void Terminate();
    public abstract string GetDescription();

    public virtual void Start()
    {
        MakeImageComponent();
    }

    private void MakeImageComponent()
    {
        gameObject.AddComponent<Image>();
        _imageComponent = GetComponent<Image>();
        _imageComponent.sprite = Icon;
    }
}