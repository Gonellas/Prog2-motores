using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData item;

    private void Update()
    {
        //InventorySystem.current.DebugPrintDictionary();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (item == null)
        {
            Debug.LogError("�El �tem en ItemObject es nulo!");
            return;
        }

        if (other.CompareTag("Player") )
        {
            AddToInventory();
        }
    }
    private void AddToInventory()
    {
        InventorySystem.current.Add(item);
        Debug.Log("Se agreg� el �tem");
        Destroy(gameObject);
    }
}
