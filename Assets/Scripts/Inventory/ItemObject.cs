using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpType
{
    ShieldBoost,
    SpeedBoost,
    LifeBoost
}

public class ItemObject : MonoBehaviour
{
    public InventoryItemData item;

    [SerializeField] Transform playerTransform;
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < 2.0f && Input.GetKeyDown(KeyCode.Q))
        {
            AddItemToInventory();
        }
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
