using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] float maxHealth = 100;
 
    public float currentHealth;

    public bool canTakeDamage = true;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealthBar()
    {
        currentHealth = Mathf.Max(0, currentHealth);

        healthBar.value = (float)currentHealth / maxHealth;
    }

    public void DisableDamage()
    {
        canTakeDamage = false;
    }

    public void EnableDamage()
    {
        canTakeDamage = true;
    }
}

