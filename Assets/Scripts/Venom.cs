using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : Traps
{
    [SerializeField] private float _damageOverTime = 1f;
    [SerializeField] bool _isPlayerInVenom = false;    

    private void Update()
    {
        if (_isPlayerInVenom)
        {
            StartCoroutine(ApplyVenom());
        }

        if (playerHealth.currentHealth <= 0) base.Die();
    }

    private IEnumerator ApplyVenom()
    {
        if (_canTakeDamage)
        {
            base.TakeDamage(_damageOverTime * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _isPlayerInVenom = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _isPlayerInVenom = false;
    }

}
