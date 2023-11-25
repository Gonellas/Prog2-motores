using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float _arrowSpeedZ = 2.0f; 

    [SerializeField]
    private string _tag;

    public bool _moveRight = true; 

    private void Update()
    {
        float direccionZ = _moveRight ? 1.0f : -1.0f;

        transform.Translate(Vector3.forward * _arrowSpeedZ * direccionZ * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(_tag))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();

        if (player != null) player.TakeDamage(10);

        Destroy(gameObject);
    }
}