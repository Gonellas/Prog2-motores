using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        float horisontalAxis = Input.GetAxis("Horizontal");

        Vector3 horizontalDirection = transform.right * horisontalAxis;

        Vector3 direction = horizontalDirection;

        

    }
}
