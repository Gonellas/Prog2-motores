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
    [SerializeField] private float _fallVelocity;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpForce = 8f;

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

    //public void Jump()
    //{
    //    if (player.isGrounded && Input.GetButtonDown("Jump"))
    //    {
    //        _fallVelocity = _jumpForce;
    //        movePlayer.y = _fallVelocity;

    //    }
    //}
    private void Rotate(Vector3 dir)
    {
        transform.forward = dir;
    }
}
