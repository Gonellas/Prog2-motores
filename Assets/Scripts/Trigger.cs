using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject _message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _message.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _message.SetActive(false);
    }
}
