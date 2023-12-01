using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Values")]
    public float _speed;
    [SerializeField] float _gravity;
    [SerializeField] float _jumpForce;
    [SerializeField] float _groundDistance;
    [SerializeField] bool _isGrounded;

    Rigidbody _rb;
    Vector3 _newDir;
    Vector3 _dir;
    PlayerController _controller;
    PlayerMovement _movement;
    PlayerView _view;
    PlayerHealth playerHealth;

    public InventoryItemData healthItem;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform _camTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();

        _movement = new PlayerMovement(transform, _camTransform, groundMask, _rb, _newDir, _view, _speed, _gravity, _jumpForce, _groundDistance, _isGrounded);
        _controller = new PlayerController(_movement, _dir);

        _movement.ArtificialAwake();
    }

    private void FixedUpdate()
    {
        _controller.ListenFixedKeys();
        _movement.ArtificialFixedUpdate();
        _movement.SetMovementSpeed(_speed);
        playerHealth.UpdateHealth(playerHealth.currentHealth);
    }

    private void Update()
    {
        _controller.ArtificialUpdate();
    }
}
