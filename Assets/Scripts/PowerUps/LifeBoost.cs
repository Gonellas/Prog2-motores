using UnityEngine;

public class LifeBoost : Power
{
    public PowerUpType Type => PowerUpType.LifeBoost;

    private string _lifeID = "1";
    private string _lifeDisplayName = "LifeBoost";
    public PlayerHealth playerHealth;

    protected override void PowerDetails(InventoryItemData itemData)
    {
        _lifeID = itemData.id;
        _lifeDisplayName = itemData.displayName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ApplyItemEffect(_lifeID, _lifeDisplayName);
        }
    }

    private void LifeIncrease()
    {
        if (playerHealth.currentHealth < 100)
        {
            playerHealth.currentHealth = 100;
            playerHealth.UpdateHealthBar();
        }
    }

    protected override void ApplyPower()
    {
        LifeIncrease();
    }
}