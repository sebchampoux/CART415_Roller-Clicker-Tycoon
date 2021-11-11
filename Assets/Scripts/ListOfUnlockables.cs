using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfUnlockables<T> where T : IUnlockable
{
    public Park Park { get; set; }
    public T[] Items { get; set; }
    public IEnumerable<T> AllItems => Items;

    public IEnumerable<T> AvailableItems
    {
        get => MakeListOfAvailableItems();
    }

    public IEnumerable<T> LockedItems
    {
        get
        {
            IList<T> result = new List<T>();
            foreach (T item in Items)
            {
                if (Park.GuestsCount < item.GuestsToUnlock)
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
        foreach (T item in Items)
        {
            if (Park.GuestsCount >= item.GuestsToUnlock)
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
