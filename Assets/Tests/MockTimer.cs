using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockTimer : Timer
{
    public override void Start()
    {
        // Do not start the timer!
    }

    public void ElapseDay()
    {
        InvokeNewDay();
    }

    public void ElapseMonth()
    {
        InvokeNewMonth();
    }

    public void ElapseYear()
    {
        InvokeNewYear();
    }
}
