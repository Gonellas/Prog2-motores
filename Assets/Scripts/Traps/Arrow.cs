using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : Traps
{
    [SerializeField] private float _arrowSpeedZ = 2.0f; 
    [SerializeField] private string _wall;
    [SerializeField] private bool _moveRight = true;
    [SerializeField] private bool _rotateOnX = false;

    new private void Start()
    {
        base.Start();

        if (_rotateOnX)
        {
            transform.rotation = Quaternion.Euler(180f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
    private void Update()
    {
        float direccionZ = _moveRight ? 1.0f : -1.0f;

        transform.Translate(Vector3.forward * _arrowSpeedZ * direccionZ * Time.deltaTime);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(_wall))
        {
            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Player"))
        {
            base.TakeDamage(10);
            Destroy(gameObject);

            if (playerHealth.currentHealth <= 0)
            {
                base.Die();
            }
        }
    }

}