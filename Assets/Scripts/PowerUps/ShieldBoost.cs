using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoost : Power
{
    public PowerUpType Type => PowerUpType.ShieldBoost;
    public PlayerHealth playerHealth;
    [SerializeField] float shieldDuration = 5.0f;
    private string shieldID = "2";
    private string shieldDisplayName = "ShieldBoost";
    public GameObject _shield;

    private void Start()
    {
        InventoryItemData myItemData = FindObjectOfType<InventoryItemData>();
        if (myItemData != null)
        {
            shieldID = myItemData.id;
            shieldDisplayName = myItemData.displayName;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventorySystem.current.RemoveItemFromInventory(shieldID, shieldDisplayName);
            ApplyItemEffect();
            Debug.Log("Se aplico");
        }
    }

    public override void ApplyPower()
    {
        StartCoroutine(ActivateShield());
        
    }

    private IEnumerator ActivateShield()
    {
        if (playerHealth != null)
        {
            playerHealth.DisableDamage();
            _shield.SetActive(true);
            yield return new WaitForSeconds(shieldDuration);
            playerHealth.EnableDamage();
            _shield.SetActive(false);

        }
        else
        {
            Debug.LogError("PlayerHealth no está asignado en ShieldBoost");
        }
    }

    public void ApplyItemEffect()
    {
        if (InventorySystem.current != null)
        {
            if (InventorySystem.current.HasItemWithDetails(shieldID, shieldDisplayName))
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