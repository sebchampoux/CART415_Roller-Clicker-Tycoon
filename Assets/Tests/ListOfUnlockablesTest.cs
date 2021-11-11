using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ListOfUnlockablesTest
{
    private ListOfUnlockables<ConcreteUnlockable> _listOfUnlockables;
    private SpecificMockPark _park;
    private ConcreteUnlockable[] _unlockables;

    private class SpecificMockPark : Park
    {
        public void SetGuestsCount(int guests)
        {
            GuestsCount = guests;
        }
    }

    private class ConcreteUnlockable : IUnlockable
    {
        public ConcreteUnlockable(int guestsToUnlock)
        {
            GuestsToUnlock = guestsToUnlock;
        }
        public int GuestsToUnlock { get; set; }
    }

    [SetUp]
    public void SetupEachTest()
    {
        GameObject temp = new GameObject();

        temp.AddComponent<SpecificMockPark>();
        _park = temp.GetComponent<SpecificMockPark>();
        _park.SetGuestsCount(250);

        _unlockables = new ConcreteUnlockable[]
        {
            new ConcreteUnlockable(100),
            new ConcreteUnlockable(150),
            new ConcreteUnlockable(250),
            new ConcreteUnlockable(400),
            new ConcreteUnlockable(500)
        };

        _listOfUnlockables = new ListOfUnlockables<ConcreteUnlockable>(_unlockables, _park);
    }

    [Test]
    public void shouldReturnEntireListCorrectly()
    {
        IEnumerable<IUnlockable> list = _listOfUnlockables.AllItems;
        IEnumerator<IUnlockable> it = list.GetEnumerator();
        for(int i = 0; i < _unlockables.Length; i++)
        {
            it.MoveNext();
            Assert.AreEqual(_unlockables[i], it.Current);
        }
        Assert.IsFalse(it.MoveNext());
    }

    [Test]
    public void shouldReturnUnlockedItemsCorrectly()
    {
        IEnumerable<IUnlockable> list = _listOfUnlockables.AvailableItems;
        IEnumerator<IUnlockable> it = list.GetEnumerator();
        for (int i = 0; i < 3; i++)
        {
            it.MoveNext();
            Assert.AreEqual(_unlockables[i], it.Current);
        }
        Assert.IsFalse(it.MoveNext());
    }

    [Test]
    public void shouldReturnLockedItemsCorrectly()
    {
        IEnumerable<IUnlockable> list = _listOfUnlockables.LockedItems;
        IEnumerator<IUnlockable> it = list.GetEnumerator();
        for (int i = 3; i < _unlockables.Length; i++)
        {
            it.MoveNext();
            Assert.AreEqual(_unlockables[i], it.Current);
        }
        Assert.IsFalse(it.MoveNext());
    }

    [Test]
    public void shouldReturnARandomAvailableItem()
    {
        IUnlockable item = _listOfUnlockables.GetARandomAvailableItem();
        Assert.GreaterOrEqual(_park.GuestsCount, item.GuestsToUnlock);
    }
}
