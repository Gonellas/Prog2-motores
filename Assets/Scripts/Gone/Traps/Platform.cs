using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;
    public float fallSpeed = 1.0f;
    public float fallDistance = 3.0f;
    private Vector3 initialPosition;
    private bool isFalling = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isFalling)
        {
            Vector3 newPosition = transform.position;
            newPosition.y -= fallSpeed * Time.deltaTime;
            transform.position = newPosition;

            // Verifica si la plataforma ha caído lo suficiente
            if (transform.position.y < initialPosition.y - fallDistance)
            {
                // La plataforma ha caído lo suficiente, puedes realizar una acción, como desactivarla o destruirla.
                // Ejemplo:
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            isFalling = true;
        }
    }
}
