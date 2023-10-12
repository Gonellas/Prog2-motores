using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    [SerializeField]
    int maxHealth = 100;

    [SerializeField]
    int currentHealth;

    SceneManagerController sceneManagerController;

    bool _canTakeDamage = true;

    private void Start()
    {
        sceneManagerController = FindObjectOfType<SceneManagerController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage)
        {
            currentHealth -= damage;
            UpdateHealthBar();  
            if (currentHealth <= 0)
            {
                sceneManagerController.RestartLevel();
            }
        }

        Debug.Log("Daño recibido");
    }

    private void UpdateHealthBar()
    {
        currentHealth = Mathf.Max(0, currentHealth);


        healthBar.value = (float)currentHealth / maxHealth;
    }

    public void DisableDamage()
    {
        _canTakeDamage = false;
    }

    public void EnableDamage()
    {
        _canTakeDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage(10); 
        }
    }

}

