using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traps: MonoBehaviour, IDamageable
{
     bool _canTakeDamage = true;
    [SerializeField] GameObject trap;

    public SceneManagerController sceneManagerController;
    public PlayerHealth playerHealth;

    public void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        sceneManagerController = FindObjectOfType<SceneManagerController>();
    }
    public virtual void TakeDamage(float dmg) 
    {
        if (_canTakeDamage)
        {
            playerHealth.currentHealth -= dmg;
            playerHealth.UpdateHealthBar();           
        }
    }

    public virtual void Die()
    {
        if (playerHealth.currentHealth <= 0)
        {
            sceneManagerController.RestartLevel();
        }
    }

    public virtual void Activator()
    {
        trap.SetActive(true);
    }

    public virtual void CheckPlayerHeight(Transform player, float deathYPosition)
    {
        float playerYPosition = player.position.y;

        if (player.position.y <= deathYPosition)
        {
            StartCoroutine(DeathTimer());
        }
    }

    public virtual IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(1.5f);
        TakeDamage(100);
        Die();
    }
}
