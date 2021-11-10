using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfUnlockables : MonoBehaviour
{
    public Park Park { get; set; }
    public IUnlockable[] Items { get; set; }
    public IEnumerable<IUnlockable> AllItems => Items;

    public IEnumerable<IUnlockable> AvailableItems
    {
        get
        {
            IList<IUnlockable> result = new List<IUnlockable>();
            foreach(IUnlockable item in Items)
            {
                if (Park.GuestsCount >= item.GuestsToUnlock)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }

    public IEnumerable<IUnlockable> LockedItems
    {
        get
        {
            IList<IUnlockable> result = new List<IUnlockable>();
            foreach (IUnlockable item in Items)
            {
                if (Park.GuestsCount < item.GuestsToUnlock)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
