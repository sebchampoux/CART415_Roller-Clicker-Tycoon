using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfUnlockables<T> where T : IUnlockable
{
    private T[] _items;
    private Park _park;

    public ListOfUnlockables(T[] items, Park park)
    {
        _items = items;
        _park = park;
    }

    public IEnumerable<T> AllItems => _items;

    public IList<T> AvailableItems
    {
        get => MakeListOfAvailableItems().AsReadOnly();
    }

    public IList<T> LockedItems
    {
        get => MakeListOfLockedItems().AsReadOnly();
    }

    private List<T> MakeListOfLockedItems()
    {
        List<T> result = new List<T>();
        foreach (T item in _items)
        {
            if (!isAvailable(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    private List<T> MakeListOfAvailableItems()
    {
        List<T> result = new List<T>();
        foreach (T item in _items)
        {
            if (isAvailable(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    private bool isAvailable(T item)
    {
        return _park.GuestsCount >= item.GuestsToUnlock && _park.Bankroll >= item.InitialCost;
    }

    public T GetARandomAvailableItem()
    {
        IList<T> availableItems = MakeListOfAvailableItems();
        int randomIndex = (int)UnityEngine.Random.Range(0, availableItems.Count - 1);
        return availableItems[randomIndex];
    }
}
