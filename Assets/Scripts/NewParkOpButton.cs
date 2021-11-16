using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewParkOpButton : MonoBehaviour
{
    public Image image;
    public Text nameTextField;
    public Text descriptionTextField;
    private ParkOperation _item;

    public ParkOperation Item
    {
        get => _item;
        set
        {
            _item = value;
            UpdateItemDescription();
        }
    }

    private void UpdateItemDescription()
    {
        nameTextField.text = _item.Name;
        descriptionTextField.text = _item.GetDescription();
        image.sprite = _item.Icon;
    }
}
