using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShopsModal : NewParkItemModal
{
    public override void AddNewElementToPark(ParkOperation item)
    {
        Shop shopPrefab = item.GetComponent<Shop>();
        _park.AddNewShop(shopPrefab);
    }
}
