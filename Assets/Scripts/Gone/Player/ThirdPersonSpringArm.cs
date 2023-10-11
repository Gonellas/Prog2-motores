using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonSpringArm : MonoBehaviour
{
    Camera _camRef;

    [SerializeField]
    float _mouseXRot;

    [SerializeField]
    float _mouseYRot;

    [SerializeField]
    float _mouseCameraRot = 360;

    [SerializeField]
    [Range(0,10)]
    float _mouseSensitivityX = 2f;

    [SerializeField]
    [Range(0, 10)]
    float _mouseSensitivityY = 2f;

    [SerializeField]
    float _maxCamConeView = 60;

    [SerializeField]
    float _armLenght = 10;

    [SerializeField]
    LayerMask _armMask;

    [SerializeField]
    float _camSpeedAdjust = 2;

    [SerializeField]
    float _onHitOffset = 0.5f;

    Ray _rayToCamera;
    Vector3 _dirToCamera;
    RaycastHit _raycastHit;

    bool _isCameraBlocked;

    private void Awake()
    {
        _camRef = GetComponentInChildren<Camera>();

        //Si no encuentra la cámara destruye el spring arm
        if (_camRef == null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _mouseSensitivityX *= 100;
        _mouseSensitivityY *= 100;
    }
    private void FixedUpdate()
    {
        //Obtenemos la dirección entre dos ubicaciones y la normalizamos
        _dirToCamera = (_camRef.transform.position - transform.position).normalized;

        //Creamos el rayo que vamos a tirar, requiere una ubicación de origen y una dirección
       _rayToCamera = new Ray(transform.position, _dirToCamera);

        //Lanzamos el rayo y nos guardamos el resultado si golpeo o no
        _isCameraBlocked = Physics.Raycast(_rayToCamera, out _raycastHit, _armLenght, _armMask);
    }

    private void Update()
    {
        
    }

    void LateUpdate()
    {
        //Sumatoria del movimiento del mouse en cada eje
        _mouseXRot += Input.GetAxisRaw("Mouse X") * _mouseSensitivityX * Time.deltaTime;
        _mouseYRot += Input.GetAxisRaw("Mouse Y") * _mouseSensitivityY * Time.deltaTime;

        if (Mathf.Abs(_mouseXRot) >= _mouseCameraRot)
        {
            _mouseXRot -= _mouseCameraRot * Mathf.Sign(_mouseXRot);
        }

        //Limitamos rotación vertical de la cámara según el ángulo que decidimos
        _mouseYRot = Mathf.Clamp(_mouseYRot, -_maxCamConeView, _maxCamConeView);

        //Seteamos la rotaci´n final del springArm sin eje z
        transform.rotation = Quaternion.Euler(-_mouseYRot, _mouseXRot, 0);

        //Asignamos nueva posición
        float newCameraLocation = -_armLenght;

        if (_isCameraBlocked) newCameraLocation = -_raycastHit.distance +_onHitOffset;

        newCameraLocation = Mathf.Lerp(_camRef.transform.localPosition.z, newCameraLocation, _camSpeedAdjust * Time.deltaTime);

        //La cámara mira directo al springarm
        _camRef.transform.localPosition = new Vector3( 0, 0, newCameraLocation);
        _camRef.transform.LookAt(transform.position);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(transform.position, 0.5f);

        Gizmos.color = Color.green;

        if (_camRef) Gizmos.DrawSphere(_camRef.transform.position, 0.5f);

        Gizmos.color = _isCameraBlocked ? Color.red : Color.cyan;

        if (_camRef) Gizmos.DrawLine(transform.position, _camRef.transform.position);
    }
}
