using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 100;

    [SerializeField]
    int currentHealth;

    Traps traps;
    SceneManagerController sceneManagerController;

    bool _canTakeDamage = true;

    private void Start()
    {
        traps = GetComponent<Traps>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage)
        {

            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                sceneManagerController.RestartLevel();
            }
        }
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
            traps.TakeDamage(10); 
        }
    }

}

