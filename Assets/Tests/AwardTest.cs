using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AwardTest
{
    private Award _award;

    [SetUp]
    public void BeforeEachTest()
    {
        GameObject t = new GameObject();
        t.AddComponent<Award>();
        _award = t.GetComponent<Award>();
    }

    [Test]
    public void shouldHaveNameProp()
    {
        Assert.IsNotNull(_award.Name);
    }

    [Test]
    public void shouldHaveDescriptionProp()
    {
        Assert.IsNotNull(_award.Description);
    }

    [Test]
    public void shouldHaveGuestsToUnlockProp()
    {
        Assert.GreaterOrEqual(0f, _award.GuestsToUnlock);
    }
}
