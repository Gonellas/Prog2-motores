using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour, IInteractable
{
    InventoryItemData _item; // El objeto Inventory ItemData que deseas asignar

    public virtual void AddToInventory()
    {
        InventorySystem.current.Add(_item);
        Debug.Log("Se agregp");
        Destroy(gameObject);
    }
}
