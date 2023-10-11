using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    //Controller _controller;
    //Movement _movement;
    [SerializeField]
    float _playerSpeed = 2;

    [SerializeField]
    float _rotSpeed = 2;

    [SerializeField]
    float _jumpForce = 2;

    Vector3 _dir; 
    Rigidbody _rb;
    Transform _camTransform;

    private void Awake()
    {
        _camTransform = GetComponentInChildren<Camera>().transform;
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //_movement = new Movement(transform, speed);
        //_controller = new Controller(_movement);

        //Obtengo distancia hasta el piso
    }

    void Update()
    {
        //_controller.ArtificialUpdate();

        float aXHorizontal = Input.GetAxisRaw("Horizontal");
        float aXVertical = Input.GetAxisRaw("Vertical");

        Vector3 _camForward2D = _camTransform.forward;
        _camForward2D.y = 0;
        _camForward2D =_camForward2D.normalized;


        Vector3 forwardDir = _camForward2D * aXVertical;
        Vector3 horizontalDir = _camTransform.right * aXHorizontal;

        _dir = forwardDir + horizontalDir;
        _dir = _dir.normalized;

        
        Jump();
           
   
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerRotation();        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    void PlayerMove()
    {
        _rb.velocity = _dir * _playerSpeed;
    }

    void PlayerRotation()
    {
        _dir.y = 0;
        _dir = _dir.normalized;

        Vector3 newRotation = Vector3.Lerp(transform.forward, _dir, _rotSpeed * Time.deltaTime);

        transform.forward = newRotation;
    }
}
