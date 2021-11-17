using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NewParkOpButton : MonoBehaviour
{
    public Image image;
    public Text nameTextField;
    public Text descriptionTextField;
    public Button ButtonComponent { get; private set; }
    private ParkOperation _item;

    public void Awake()
    {
        ButtonComponent = GetComponent<Button>();
    }

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
