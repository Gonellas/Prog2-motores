using UnityEngine;

//TPFinal Emi Cassarino - Camila Gonella
public abstract class Power : MonoBehaviour
{
    protected void Start()
    {
        InventoryItemData myItemData = FindObjectOfType<InventoryItemData>();
        if (myItemData != null)
        {
            PowerDetails(myItemData);
        }
    }

    protected abstract void PowerDetails(InventoryItemData itemData);

    protected void ApplyItemEffect(string itemId, string itemDisplayName)
    {
        if (InventorySystem.current != null)
        {
            if (InventorySystem.current.HasItemWithDetails(itemId, itemDisplayName))
            {
                ApplyPower();
                InventorySystem.current.RemoveItemFromInventory(itemId, itemDisplayName);
            }
            else
            {
                Debug.Log("El ítem no está en el inventario");
            }
        }
        else
        {
            Debug.Log("El inventario no está inicializado");
        }
    }

    protected abstract void ApplyPower();
}
