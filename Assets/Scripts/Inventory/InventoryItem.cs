using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public GameObject _icon { get; set; }

    public InventoryItem(InventoryItemData itemData)
    {
        data = itemData;
        stackSize = 1;  
        Debug.Log("Se agregó " + data + ", stack size: " + stackSize);
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
