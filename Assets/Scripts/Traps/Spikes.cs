using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TPFinal Camila Gonella
public class Spikes : Traps
{
    [SerializeField] float _maxHeight = 3.38f; 
    [SerializeField] float _minHeight = -3.38f; 
    [SerializeField] float _velocity = 1.0f; 
    [SerializeField] float _cooldown = 2.0f;

    Transform spikes; 
    Vector3 initialPos;

    new private void Start()
    {
        base.Start();

        spikes = gameObject.transform;

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
            TakeDamage(20);

            Debug.Log("Vida restante" + " " + playerHealth.currentHealth);

            if (playerHealth.currentHealth <= 0)
            {
                base.Die();
            }
        }
    }
}
