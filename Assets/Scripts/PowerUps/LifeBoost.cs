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
            InventorySystem.current.RemoveItemFromInventory(lifeID, lifeDisplayName);
            ApplyItemEffect();
            Debug.Log("sumo vida");
        }
    }

    private IEnumerator LifeIncrease()
    {
        yield return new WaitForSeconds(2f); 

        if (playerHealth != null)
        {
            if (playerHealth.currentHealth < 100)
            {
                playerHealth.currentHealth += _increaseHealth;
                if (playerHealth.currentHealth > 100)
                {
                    playerHealth.currentHealth = 100;
                }
                playerHealth.UpdateHealthBar();
            }
        }

    }
    //private void LifeIncrease()
    //{
    //    if (playerHealth.currentHealth < 100)
    //    {
    //        playerHealth.currentHealth = 100;
    //        playerHealth.UpdateHealthBar();
    //    }

    //    //if (playerHealth.currentHealth >= 100)
    //    //{
    //    //    playerHealth.currentHealth = 100;
    //    //}
    //}
    public override void ApplyPower()
    {
        StartCoroutine(LifeIncrease());
    }


    public void ApplyItemEffect()
    {
        if (InventorySystem.current != null)
        {
            if (InventorySystem.current.HasItemWithDetails(lifeID, lifeDisplayName))
            {
                ApplyPower();
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
