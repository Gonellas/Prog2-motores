using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traps : MonoBehaviour
{
    [SerializeField] bool _canTakeDamage = true;
    [SerializeField] float _currentHealth;

    PlayerHealth playerHealth;
    SceneManagerController sceneManagerController;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        sceneManagerController = GetComponent<SceneManagerController>();

    }
    public void MakeDamage(float damage) 
    {
        if (_canTakeDamage)
        {
            _currentHealth -= damage;
            playerHealth.UpdateHealthBar();
            if (_currentHealth <= 0)
            {
                sceneManagerController.RestartLevel();
            }
        }

        Debug.Log("Daño recibido");
    }

    
}
