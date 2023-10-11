using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    [SerializeField]
    float _arrowSpeedZ = 2.0f; // Velocidad de movimiento en el eje Z

    [SerializeField]
    string _tag;

    public bool _moveRight = true; // Control de direcci�n de movimiento

    private void Update()
    {
        // Calcula la direcci�n de movimiento en el eje Z
        float direccionZ = _moveRight ? 1.0f : -1.0f;

        // Mueve la flecha en el eje Z
        transform.Translate(Vector3.forward * _arrowSpeedZ * direccionZ * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(_tag))
        {
            Destroy(gameObject);
        }
    }


}