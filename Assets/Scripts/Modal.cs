using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Modal : MonoBehaviour
{
    [SerializeField] protected Park _park;
    [SerializeField] private ParkOperation[] _items;
    [SerializeField] private NewParkOpButton _newItemButtonPrefab;
    [SerializeField] private VerticalLayoutGroup _scrollViewContent;

    private ListOfUnlockables<ParkOperation> _listOfItems;
    private IList<NewParkOpButton> _itemsButtons = new List<NewParkOpButton>();

    public void Start()
    {
        _listOfItems = new ListOfUnlockables<ParkOperation>(_items, _park);
        CreateItemsButtons();
        SubscribeToParkEvents();
    }

    private void CreateItemsButtons()
    {
        foreach (ParkOperation item in _listOfItems.AllItems)
        {
            NewParkOpButton newItemButton = Instantiate(_newItemButtonPrefab);
            newItemButton.transform.SetParent(_scrollViewContent.transform);
            newItemButton.transform.localScale = new Vector3(1f, 1f);
            newItemButton.Item = item;
            newItemButton.ButtonComponent.onClick.AddListener(() => AddNewElementToPark(item));
            _itemsButtons.Add(newItemButton);
        }
        LayoutRebuilder.MarkLayoutForRebuild(_scrollViewContent.gameObject.GetComponent<RectTransform>());
        UpdateAvailableItemsList(this, null);
    }

    private void SubscribeToParkEvents()
    {
        _park.OnGuestsCountChange += UpdateAvailableItemsList;
        _park.OnBankrollChange += UpdateAvailableItemsList;
    }

    private void UpdateAvailableItemsList(object sender, EventArgs e)
    {
        IList<ParkOperation> available = _listOfItems.AvailableItems;
        foreach (NewParkOpButton itemBtn in _itemsButtons)
        {
            itemBtn.ButtonComponent.interactable = available.Contains(itemBtn.Item);
        }
    }

    public void OpenModal()
    {
        gameObject.SetActive(true);
    }

    public void CloseModal()
    {
        gameObject.SetActive(false);
    }

    public abstract void AddNewElementToPark(ParkOperation item);
}
