using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AwardsManagerTest
{
    public class TheMockPark : Park
    {
        public Award _addAwardLastCalledWith = null;

        public void SetGuestsCount(int count)
        {
            GuestsCount = count;
        }

        public override void AddAward(Award award)
        {
            _addAwardLastCalledWith = award;
        }
    }

    public class MockAward : Award
    {
        public MockAward(int guestsToUnlock)
        {
            _guestsToUnlock = guestsToUnlock;
        }
    }

    private AwardsManager _awardsManager;
    private Award[] _awardsArray;
    private TheMockPark _park;

    [SetUp]
    public void BeforeEachTest()
    {
        GameObject t = new GameObject();
        t.AddComponent<AwardsManager>();
        _awardsManager = t.GetComponent<AwardsManager>();

        t.SetActive(false);
        _awardsArray = new Award[] {
            new MockAward(10),
            new MockAward(50),
            new MockAward(100)
        };
        _awardsManager.Awards = _awardsArray;
        t.SetActive(true);

        t.AddComponent<TheMockPark>();
        _park = t.GetComponent<TheMockPark>();
        _awardsManager._park = _park;
    }

    [Test]
    public void shouldGiveOutAwardWhenThresholdReached()
    {
        Assert.IsNull(_park._addAwardLastCalledWith);
        _park.SetGuestsCount(11);
        Assert.AreEqual(_awardsArray[0], _park._addAwardLastCalledWith);
        _park.SetGuestsCount(50);
        Assert.AreEqual(_awardsArray[1], _park._addAwardLastCalledWith);
        _park.SetGuestsCount(150);
        Assert.AreEqual(_awardsArray[2], _park._addAwardLastCalledWith);
    }
}
