using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private PlayerMovement2 _model;
    private Vector3 _dir;

    private void Awake()
    {
        _model = GetComponent<PlayerMovement2>();
        _dir = new Vector3();
    }

    //public void ListenKeys()
    //{

    //}

    public void ListenFixedKeys()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");

        _model.Movement(_dir);
    }
}








