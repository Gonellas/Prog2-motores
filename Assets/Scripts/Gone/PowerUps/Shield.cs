using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUps
{
    [SerializeField]
    float shieldDuration = 5.0f;

    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public override void ApplyPowerUp()
    {
        StartCoroutine(ActivateShield());
    }

    private IEnumerator ActivateShield()
    {
        playerHealth.DisableDamage();

        yield return new WaitForSeconds(shieldDuration);

        playerHealth.EnableDamage(); 
    }
}
