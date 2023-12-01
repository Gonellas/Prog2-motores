using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    [Header("Components")]
    [SerializeField] private Transform _camTransform;
    [SerializeField] private LayerMask _groundMask;

    [Header("Values")]
    public float _movSpeed = 5f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _groundDistance = 1.3f;
    [SerializeField] private bool _isGrounded;

    Rigidbody _rb;
    PlayerView _view;
    Vector3 _newDir;
    Transform _transform;

    public PlayerMovement(Transform transform, Transform camTransform, LayerMask groundMask, Rigidbody rb, Vector3 newDir, PlayerView view, float speed, float gravity, float jumpForce, float groundDistance, bool isGrounded)
    {
        _transform = transform;
        _camTransform = camTransform;
        _groundMask = groundMask;
        _rb = rb;
        _view = view;
        _movSpeed = speed;
        _gravity = gravity;
        _jumpForce = jumpForce;
        _groundDistance = groundDistance;
        _isGrounded = isGrounded;
        _newDir = newDir;

    }
    public void ArtificialAwake()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.angularDrag = 1f;

        _view = _rb.gameObject.GetComponent<PlayerView>();
    }

    public void ArtificialFixedUpdate()
    {
        ApplyGravity();
    }

    public void Movement(Vector3 dir)
    {
        if (dir.sqrMagnitude != 0)
        {
            Vector3 camForwardFixed = _camTransform.forward;
            Vector3 camRightFixed = _camTransform.right;

            camForwardFixed.y = 0f;
            camRightFixed.y = 0f;

            Rotate(camForwardFixed);

            if (dir.x != 0 || dir.z != 0)
            {
                _newDir = (camRightFixed * dir.x + camForwardFixed * dir.z).normalized;
            }

            _rb.MovePosition(_transform.position + _newDir * _movSpeed * Time.fixedDeltaTime);
        }

        _view.SetMovement(dir.x, dir.z);
    }

    public void Jump(Vector3 dir)
    {

        _isGrounded = Physics.CheckSphere(_transform.position, _groundDistance, _groundMask);

        if (_isGrounded)
        {
            Debug.Log("No toy grounded");

            // Resetear la velocidad en y para evitar el doble salto
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _view.SetJumping(true);
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
        _transform.forward = dir;
    }

    public void SetMovementSpeed(float speed)
    {
        _movSpeed = speed;
    }
}
