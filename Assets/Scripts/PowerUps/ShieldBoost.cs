using System.Collections;
using UnityEngine;

//TPFinal Emi Cassarino - Camila Gonella

public class ShieldBoost : Power
{
    public PlayerHealth playerHealth;
    [SerializeField] private float _shieldDuration = 5.0f;
    private string _shieldID = "2";
    private string _shieldDisplayName = "ShieldBoost";
    public GameObject _shield;

    protected override void PowerDetails(InventoryItemData itemData)
    {
        _shieldID = itemData.id;
        _shieldDisplayName = itemData.displayName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ApplyItemEffect(_shieldID, _shieldDisplayName);
        }
    }

    protected override void ApplyPower()
    {
        StartCoroutine(ActivateShield());
    }

    private IEnumerator ActivateShield()
    {
        if (playerHealth != null)
        {
            playerHealth.canTakeDamage = false;
            _shield.SetActive(true);
            playerHealth.DisableDamage();
            yield return new WaitForSeconds(_shieldDuration);
            _shield.SetActive(false);
            playerHealth.EnableDamage();
        }
    }
}