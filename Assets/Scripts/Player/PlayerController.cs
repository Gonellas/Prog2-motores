using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    PlayerMovement _movement;
    Vector3 _dir;

    public PlayerController(PlayerMovement movement, Vector3 dir)
    {
        _movement = movement;
        _dir = dir;
    }


    public void ArtificialUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
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
