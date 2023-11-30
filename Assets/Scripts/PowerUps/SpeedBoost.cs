using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Power
{
    public PowerUpType Type => PowerUpType.SpeedBoost;

    [SerializeField]
    float _speedBoost = 5;
    private string speedID = "3";
    private string speedDisplayName = "SpeedBoost";
    public GameObject speedInterface;

    public PlayerMovement _movement;
    public Player _playerSpeed;

    private void Start()
    {
        InventoryItemData myItemData = FindObjectOfType<InventoryItemData>();
        if (myItemData != null)
        {
            speedID = myItemData.id;
            speedDisplayName = myItemData.displayName;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ApplyItemEffect();
        }
    }

    public override void ApplyPower()
    {
        StartCoroutine(ActivateSpeed());
    }

    private IEnumerator ActivateSpeed()
    {
        _playerSpeed._speed += _speedBoost;
        speedInterface.SetActive(true);
        yield return new WaitForSeconds(5f);
        _playerSpeed._speed -= _speedBoost;
        speedInterface.SetActive(false);
    }

    public void ApplyItemEffect()
    {
        if (InventorySystem.current != null)
        {
            if (InventorySystem.current.HasItemWithDetails(speedID, speedDisplayName))
            {
                ApplyPower();
                InventorySystem.current.RemoveItemFromInventory(speedID, speedDisplayName);
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
