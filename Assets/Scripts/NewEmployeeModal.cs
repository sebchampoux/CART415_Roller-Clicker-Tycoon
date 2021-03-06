using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEmployeeModal : NewParkItemModal
{
    public override void AddNewElementToPark(ParkOperation item)
    {
        Employee employeePrefab = item.GetComponent<Employee>();
        _park.HireEmployee(employeePrefab);
    }
}
