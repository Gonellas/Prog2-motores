using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Transform spikes; 
    
    [SerializeField]
    float _maxHeight = 3.38f; 

    [SerializeField]
    float _minHeight = -3.38f; 

    [SerializeField]
    float _velocity = 1.0f; 

    [SerializeField]
    float _cooldown = 2.0f;

    Vector3 initialPos;
    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();

        if (spikes == null)
        {
            enabled = false;
            return;
        }

        initialPos = spikes.position;
        StartCoroutine(MoveSpikes());
    }

    private IEnumerator MoveSpikes()
    {
        while (true)
        {
            // Pinchos se muestran
            while (spikes.position.y < _maxHeight)
            {
                Vector3 newPos = spikes.position + Vector3.up * _velocity * Time.deltaTime;
                spikes.position = newPos;
                yield return null;
            }

            // Cooldown
            yield return new WaitForSeconds(_cooldown);

            // Los pinchos se esconden
            while (spikes.position.y > _minHeight)
            {
                Vector3 newPos = spikes.position + Vector3.down * _velocity * Time.deltaTime;
                spikes.position = newPos;
                yield return null;
            }

            // Cooldown pinchos
            yield return new WaitForSeconds(_cooldown);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(10); 
        }
    }
}
