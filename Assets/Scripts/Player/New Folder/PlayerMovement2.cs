using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement2 : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _camTransform;

    [Header("Values")]
    [SerializeField] private float _movSpeed = 5f;

    private Rigidbody _rb;
    private PlayerController2 _controller;
    private PlayerView _view;

    private Vector3 _newDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _controller = GetComponent<PlayerController2>();
        _view = GetComponent<PlayerView>();
    }

    //private void Update()
    //{
    //    _controller.ListenKeys();
    //}

    private void FixedUpdate()
    {
        _controller.ListenFixedKeys();
    }

    public void Movement(Vector3 dir)
    {
        if (dir.sqrMagnitude != 0)
        {
            //Obtengo el forward y right de mi camara
            Vector3 camForwardFixed = _camTransform.forward;
            Vector3 camRightFixed = _camTransform.right;

            //Si la camara mira desde abajo o arriba me estaria moviendo el personaje hacia esas drecciones
            //Por lo que simulo que la camara no este rotada en Y
            camForwardFixed.y = 0f;
            camRightFixed.y = 0f;

            //Rotamos en direccion hacia donde ve la camara
            Rotate(camForwardFixed);

            //Sumo las dos direccion multiplicadas por los respectivos inputs
            //normalizo el resultado
            _newDir = (camRightFixed * dir.x + camForwardFixed * dir.z).normalized;

            _rb.MovePosition(transform.position + _newDir * _movSpeed * Time.fixedDeltaTime);
        }

        _view.SetMovement(dir.x, dir.z);
    }

    private void Rotate(Vector3 dir)
    {
        transform.forward = dir;
    }
}
