using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : Traps
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Activator();
        }
    }
}
