﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : Traps
{
    [SerializeField] private float _arrowSpeedZ = 2.0f; 
    [SerializeField] private string _tag;
    [SerializeField] private bool _moveRight = true;

    private void Update()
    {
        float direccionZ = _moveRight ? 1.0f : -1.0f;

        transform.Translate(-Vector3.down * _arrowSpeedZ * direccionZ * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(_tag))
        {
            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Player"))
        {
            base.TakeDamage(10);
            Destroy(gameObject);

            Debug.Log("Vida restante" + " " + playerHealth.currentHealth);

            if (playerHealth.currentHealth <= 0)
            {
                base.Die();
            }
        }
    }

}