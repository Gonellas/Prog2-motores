using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement _movement;   

    private Vector3 _dir;


    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _dir = new Vector3();
    }

    public void ListenFixedKeys()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");

        //Método de mov del modelo
        _movement.Movement(_dir);


    }



    //private void SetGravity()
    //{
    //    if (player.isGrounded)
    //    {
    //        _fallVelocity = -_gravity * Time.deltaTime;
    //        movePlayer.y = _fallVelocity;
    //    }
    //    else
    //    {
    //        _fallVelocity -= _gravity * Time.deltaTime;
    //        movePlayer.y = _fallVelocity;
    //    }
    //}
}
