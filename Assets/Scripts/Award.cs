using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour, IUnlockable
{
    [SerializeField] protected int _guestsToUnlock = 0;
    [SerializeField] private string _name = "";
    [SerializeField] private string _description = "";

    public int GuestsToUnlock => _guestsToUnlock;
    public float InitialCost => 0f;
    public string Name => _name;
    public string Description => _description;
}
