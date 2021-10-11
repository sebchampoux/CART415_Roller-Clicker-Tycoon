using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaManager : MonoBehaviour
{
    public Park Park { get; set; }
    public float MonthlySalary { get; set; }

    public void OnNewYear(object sender, System.EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnNewMonth(object sender, EventArgs e)
    {
        if (Park.Bankroll >= MonthlySalary)
        {
            Park.SpendMoney(MonthlySalary);
        }
        else
        {
            Park.FurloughEmployee(this);
        }
    }
}
