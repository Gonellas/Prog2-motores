using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    [SerializeField]
    float _arrowSpeedZ = 2.0f; 

    [SerializeField]
    string _tag;

    [SerializeField]
    private PlayerHealth playerHealth; 

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
}