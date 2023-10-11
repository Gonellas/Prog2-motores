using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flechas : MonoBehaviour
{
    public Transform target;
    public float speed;

    public ActivarFlechas activarFlechas;

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        Debug.Log(" me activo fuera");
        if (activarFlechas.palito == true)
        {
            Debug.Log("Me ACTIVo dentro");
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
