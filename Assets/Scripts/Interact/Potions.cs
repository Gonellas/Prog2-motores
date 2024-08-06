using UnityEngine;

public class Potions : Interact
{
    public InventoryItemData item;
    private bool _isPickedUp = false; 

    public override void InteractionAction()
    {
        if (!_isPickedUp)
        {
            _isPickedUp = true;
            AddItemToInventory();
        }
    }

    private void AddItemToInventory()
    {
        if (InventorySystem.current != null)
        {
            InventorySystem.current.Add(item);

            if (_player != null)
            {
                IMessage interactMessage = _player.GetComponent<IMessage>();
                if (interactMessage != null) interactMessage.DeactivateUI();
            }

            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("Se agregó el ítem al inventario");
        }
        else
        {
            Debug.LogError("No se encontró el inventario");
        }
    }
}

