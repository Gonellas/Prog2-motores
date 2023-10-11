using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos : MonoBehaviour
{
    public Transform movingObject; // El objeto que quieres mover (debe estar asignado en el Inspector)
    
    [SerializeField]
    float _maxHeight = 3.38f; // Altura m�xima a la que se mover� el objeto

    [SerializeField]
    float _minHeight = -3.38f; // Altura m�nima a la que se mover� el objeto

    [SerializeField]
    float _velocity = 1.0f; // Velocidad de movimiento

    [SerializeField]
    float _cooldown = 2.0f; // Tiempo de espera entre movimientos

    Vector3 initialPos;

    private void Start()
    {
        if (movingObject == null)
        {
            enabled = false; // Desactiva el script si el objeto no est� asignado
            return;
        }

        initialPos = movingObject.position;
        // Comienza el proceso de movimiento
        StartCoroutine(MoverObjetoPeriodicamente());
    }

    private IEnumerator MoverObjetoPeriodicamente()
    {
        while (true)
        {
            // Mueve el objeto hacia arriba
            while (movingObject.position.y < _maxHeight)
            {
                Vector3 newPos = movingObject.position + Vector3.up * _velocity * Time.deltaTime;
                movingObject.position = newPos;
                yield return null;
            }

            // Espera el tiempo especificado
            yield return new WaitForSeconds(_cooldown);

            // Mueve el objeto hacia abajo
            while (movingObject.position.y > _minHeight)
            {
                Vector3 newPos = movingObject.position + Vector3.down * _velocity * Time.deltaTime;
                movingObject.position = newPos;
                yield return null;
            }

            // Espera el tiempo especificado
            yield return new WaitForSeconds(_cooldown);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) other.GetComponent<PlayerHealth>().TakeDamage(10); ;
    }
}
