using UnityEngine;

//TPFinal Camila Gonella del Carril

public class MouseLook : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _target;

    [Header("Cursor")]
    [SerializeField] private CursorLockMode _lockMode = CursorLockMode.Locked;
    [SerializeField] private bool _isCursorVisible = false;

    [Header("Physics")]
    [SerializeField] private float _hitOffset = .2f;

    [Header("Settings")]
    [Range(1f, 1000f)] [SerializeField] private float _mouseSensitivity = 100f;
    [Range(.01f, 1f)] [SerializeField] private float _detectionRadius = .1f;
    [Range(.125f, 2f)] [SerializeField] private float _minDistance = .25f;
    [Range(2f, 10f)] [SerializeField] private float _maxDistance = 3f;
    [Range(-90f, 0f)] [SerializeField] private float _minRotation = -70f;
    [Range(0f, 90f)] [SerializeField] private float _maxRotation = 70f;

    private float _mouseX, _mouseY;
    private Vector3 _dir, _camPos;

    private Ray _camRay;
    private RaycastHit _camRayHit;
    private bool _isCamBlocked;

    private void Start()
    {
        Cursor.lockState = _lockMode;
        Cursor.visible = _isCursorVisible;

        transform.forward = _target.forward;

        _mouseX = transform.eulerAngles.y;
        _mouseY = transform.eulerAngles.x;
    }

    private void FixedUpdate()
    {
        _camRay = new Ray(transform.position, _dir);

        _isCamBlocked = Physics.SphereCast(_camRay, _detectionRadius, out _camRayHit, _maxDistance);
    }

    private void LateUpdate()
    {
        UpdateCamRotation(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        UpdateSpringArm();
    }

    private void UpdateCamRotation(float x, float y)
    {
        transform.position = _target.position;

        if (x == 0 && y == 0) return;

        if (x != 0)
        {
            _mouseX += x * _mouseSensitivity * Time.deltaTime;

            if (_mouseX > 360 || _mouseX < -360)
            {
                _mouseX -= 360 * Mathf.Sign(_mouseX);
            }
        }

        if (y != 0)
        {
            _mouseY += y * _mouseSensitivity * Time.deltaTime;

            _mouseY = Mathf.Clamp(_mouseY, _minRotation, _maxRotation);
        }

        transform.rotation = Quaternion.Euler(-_mouseY, _mouseX, 0f);
    }

    private void UpdateSpringArm()
    {
        _dir = -transform.forward;

        if (_isCamBlocked)
        {
            Vector3 dirTest = (_camRayHit.point - transform.position) + (_camRayHit.normal * _hitOffset);

            if (dirTest.sqrMagnitude <= Mathf.Pow(_minDistance, 2))
            {
                _camPos = transform.position + _dir * _minDistance;
            }
            else
            {
                _camPos = transform.position + dirTest;
            }
        }
        else
        {
            _camPos = transform.position + _dir * _maxDistance;
        }

        _cam.transform.position = _camPos;
        _cam.transform.LookAt(transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        var pos = transform.position;

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(pos, .1f);
        Gizmos.DrawSphere(_cam.transform.position, .1f);
        Gizmos.DrawLine(pos, _cam.transform.position);
    }
}