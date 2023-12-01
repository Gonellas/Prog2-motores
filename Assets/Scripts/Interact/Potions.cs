using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions: Interact
{ 
    public InventoryItemData item;

    protected override void InteractionAction()
    {
        AddItemToInventory();
    }

    private void AddItemToInventory()
    {
        if (InventorySystem.current != null)
        {
            InventorySystem.current.Add(item);
            Destroy(gameObject);
            Debug.Log("Se agregó el ítem al inventario");
        }
        else
        {
            Debug.LogError("No se encontró el inventario");
        }
    }
}


