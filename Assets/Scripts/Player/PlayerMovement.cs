using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _camTransform;

    [Header("Values")]
    [SerializeField] private float _movSpeed = 5f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _groundDistance = 0.2f;
    [SerializeField] private LayerMask _groundMask;

    private bool _isGrounded;



    private Rigidbody _rb;    
    private PlayerController _controller;
    private PlayerView _view;

    private Vector3 _newDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.angularDrag = 1f;

        _controller = GetComponent<PlayerController>();
        _view = GetComponent<PlayerView>();
    }

    private void FixedUpdate()
    {
        _controller.ListenFixedKeys();
        ApplyGravity();
    }

    public void Movement(Vector3 dir)
    {
        if(dir.sqrMagnitude != 0)
        {
            Vector3 camForwardFixed = _camTransform.forward;
            Vector3 camRightFixed = _camTransform.right;

            camForwardFixed.y = 0f;
            camRightFixed.y = 0f;

            //Rotar personaje a la dir de la cámara
            Rotate(camForwardFixed);

            _newDir = (camRightFixed * dir.x + camForwardFixed * dir.z).normalized;

            _rb.MovePosition(transform.position + _newDir * _movSpeed * Time.fixedDeltaTime);
        }

        _view.SetMovement(dir.x, dir.z);
    }

    public void Jump()
    {
        _isGrounded = Physics.CheckSphere(transform.position, _groundDistance, _groundMask);

        if (_isGrounded)
        {
        Debug.Log("Saltando");
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z); // Resetear la velocidad en y para evitar el doble salto
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void ApplyGravity()
    {
        if (!_isGrounded)
        {
            _rb.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
        }
    }
    private void Rotate(Vector3 dir)
    {
        transform.forward = dir;
    }
}
