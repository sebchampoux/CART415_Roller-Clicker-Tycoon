using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwardDisplay : MonoBehaviour
{
    public Text title;
    public Text description;
    private Award _award;

    public Award award
    {
        get => _award;
        set
        {
            _award = value;
            UpdateAwardDisplay();
        }
    }
    private void UpdateAwardDisplay()
    {
        if (_award == null) return;
        title.text = _award.Name + " : " + _award.GuestsToUnlock + " guests";
        description.text = _award.Description;
    }
}
