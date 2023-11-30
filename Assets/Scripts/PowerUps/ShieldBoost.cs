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
        Debug.Log("ActivateShield");
        if (playerHealth != null)
        {
            Debug.Log("playerhealth");
            playerHealth.canTakeDamage = false;
            _shield.SetActive(true);
            playerHealth.DisableDamage();
            yield return new WaitForSeconds(shieldDuration);
            _shield.SetActive(false);
            playerHealth.EnableDamage();
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
                InventorySystem.current.RemoveItemFromInventory(shieldID, shieldDisplayName);

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