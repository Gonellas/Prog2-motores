using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float _increaseHealth = 25f;

    public float currentHealth;
    public bool canTakeDamage = true;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealthBar()
    {
        if (canTakeDamage)
        {
            currentHealth = Mathf.Max(0, currentHealth);
            healthBar.value = (float)currentHealth / maxHealth;
        } 
        else
        {
            canTakeDamage = false;
        }
    }

    public void DisableDamage()
    {
        canTakeDamage = false;
    }

    public void EnableDamage()
    {
        canTakeDamage = true;
    }

    public void UpdateHealth(float newHealth)
    {
        currentHealth = newHealth;
    }
}

