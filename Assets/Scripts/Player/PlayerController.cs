using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private bool _isGrounded;

    PlayerMovement _movement;   

    private Vector3 _dir;


    private void Awake()
    {

        _movement = GetComponent<PlayerMovement>();
        _dir = new Vector3();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _movement.Jump();
        }
    }
    public void ListenFixedKeys()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");


       

        _movement.ApplyGravity();

        //Método de mov del modelo
        _movement.Movement(_dir);

    }

    






}
