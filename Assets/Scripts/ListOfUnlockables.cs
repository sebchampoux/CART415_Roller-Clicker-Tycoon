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

    public IEnumerable<T> AvailableItems
    {
        get => MakeListOfAvailableItems();
    }

    public IEnumerable<T> LockedItems
    {
        get
        {
            IList<T> result = new List<T>();
            foreach (T item in _items)
            {
                if (_park.GuestsCount < item.GuestsToUnlock)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }

    private IList<T> MakeListOfAvailableItems()
    {
        IList<T> result = new List<T>();
        foreach (T item in _items)
        {
            if (_park.GuestsCount >= item.GuestsToUnlock)
            {
                result.Add(item);
            }
        }
        return result;
    }

    public T GetARandomAvailableItem()
    {
        IList<T> availableItems = MakeListOfAvailableItems();
        int randomIndex = (int)UnityEngine.Random.Range(0, availableItems.Count - 1);
        return availableItems[randomIndex];
    }
}
