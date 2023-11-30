using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBoost : Power
{
    public PowerUpType Type => PowerUpType.LifeBoost;

    private string lifeID = "1";
    private string lifeDisplayName = "LifeBoost";
    public PlayerHealth playerHealth;
    float _increaseHealth = 25f;

    private void Start()
    {
        InventoryItemData myItemData = FindObjectOfType<InventoryItemData>();
        if (myItemData != null)
        {
            lifeID = myItemData.id;
            lifeDisplayName = myItemData.displayName;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ApplyItemEffect();
            Debug.Log("sumo vida");
        }
    }


    private void LifeIncrease()
    {
        if (playerHealth.currentHealth < 100)
        {
            playerHealth.currentHealth = 100;
            playerHealth.UpdateHealthBar();
        }

        //if (playerHealth.currentHealth >= 100)
        //{
        //    playerHealth.currentHealth = 100;
        //}
    }
    public override void ApplyPower()
    {
        LifeIncrease();
    }


    public void ApplyItemEffect()
    {
        if (InventorySystem.current != null)
        {
            if (InventorySystem.current.HasItemWithDetails(lifeID, lifeDisplayName))
            {
                ApplyPower();
                InventorySystem.current.RemoveItemFromInventory(lifeID, lifeDisplayName);
            }
            else
            {
                Debug.Log("El ítem de escudo no está en el inventario");
            }
        }
        else
        {
            Debug.Log("El inventario no está inicializado");
        }
    }
}
