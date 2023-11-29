using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController 
{
    public Interact _interact;
    PlayerMovement _movement;
    Vector3 _dir;

    public PlayerController(PlayerMovement movement, Interact interact, Vector3 dir)
    {
        _movement = movement;
        _interact = interact;
        _dir = dir;
    }


    public void ArtificialUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("salto ii");
            _movement.Jump(_dir);
        }
      
    }
    public void ListenFixedKeys()
    {

        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");

        _movement.Movement(_dir);

        _movement.ApplyGravity();
    }
}
