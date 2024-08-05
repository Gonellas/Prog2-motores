using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Genero un "Rayo invisible" desde mi posicion hacia la direccion de la camara
        _camRay = new Ray(transform.position, _dir);

        //Paso el rayo a un SphereCast para que me genere un radio en esa direccion
        //y mediante la variable _rHit obtengo los datos contra lo que colision (si es que colisiono)
        //Ademas la ejecucion de este metodo me devuelve un booleano que lo guardo en _isCameraBlocked
        _isCamBlocked = Physics.SphereCast(_camRay, _detectionRadius, out _camRayHit, _maxDistance);
    }

    private void LateUpdate()
    {
        //Actualizar la posicion y rotacion del Socket
        UpdateCamRotation(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //Actualizar la ubicacion de la camara
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
        //Tomo la direccion contraria hacia donde mira el objeto que contiene este script
        _dir = -transform.forward;

        if (_isCamBlocked)
        {
            //Me voy a fijar si donde voy a poner la camara es demasiado cerca
            Vector3 dirTest = (_camRayHit.point - transform.position) + (_camRayHit.normal * _hitOffset);

            //Si es muy cerca, la fixeo un poco mas lejos
            if (dirTest.sqrMagnitude <= Mathf.Pow(_minDistance, 2))
            {
                _camPos = transform.position + _dir * _minDistance;
            }
            else
            {
                //Pongo la camara en la posicion donde toque algo que la bloquea
                _camPos = transform.position + dirTest;
            }
        }
        else
        {
            //Pongo la camara en la distancia correcta a mi personaje (determinado por _maxDistance)
            _camPos = transform.position + _dir * _maxDistance;
        }

        //Le paso esa posicion nueva generada
        _cam.transform.position = _camPos;
        //Y le digo a mi camara que mire hacia el personaje
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