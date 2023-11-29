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
    Interact _interact;
    InventoryItemData _item;
    ItemObject _itemOb;

    public InventoryItemData healthItem;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform _camTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _movement = new PlayerMovement(transform, _camTransform, groundMask, _rb, _newDir, _view, _speed, _gravity, _jumpForce, _groundDistance, _isGrounded);
        _interact = GetComponent<Interact>();
        _controller = new PlayerController(_movement, _interact, _dir);

        _movement.ArtificialAwake();

        _item = healthItem;
        if (_item == null)
        {
            Debug.Log("El objeto _item es nulo en Awake");
        }
    }

    private void Start()
    {
    }
    private void FixedUpdate()
    {
        _controller.ListenFixedKeys();
        _movement.ArtificialFixedUpdate();
    }

    private void Update()
    {
        _controller.ArtificialUpdate();
    }
}
