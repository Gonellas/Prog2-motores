using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController 
{
    IInteractable _interaction;
    PlayerMovement _movement;
    PlayerView _view;
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
            Debug.Log("salto ii");
            _movement.Jump(_dir);
        }
    }
    public void ListenFixedKeys()
    {

        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");

        //Método de mov del modelo
        _movement.Movement(_dir);

        _movement.ApplyGravity();
    }

    public void Interact()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _interaction.Interact();
        }
    }
}
