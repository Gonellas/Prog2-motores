using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryItem : MonoBehaviour
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public GameObject _icon { get; set; }

    public InventoryItem(InventoryItemData itemData)
    {
        Debug.Log("se agrego" + data + stackSize);
        data = itemData;
        AddToStack();
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
