using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarFlechas : MonoBehaviour
{
    public bool palito = false;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Se activa");

        if (other.CompareTag("Player"))
        {
            palito = true;
        }


    }
}
