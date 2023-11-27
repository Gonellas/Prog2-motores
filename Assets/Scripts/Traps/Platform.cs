using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Traps
{
    [SerializeField] private bool _isPlayerOnPlatform = false;
    [SerializeField] private bool _gravityActivated = false;
    [SerializeField] private float _fallTimer = 3.0f;
    [SerializeField] private float _shakeIntensity = 0.1f;
    [SerializeField] private float _shakeDuration = 2.0f;
    [SerializeField] float _deathYPosition;
    [SerializeField] Transform _player;

    Vector3 _originalPosition;
    Rigidbody _rb;

    new private void Start()
    {
        base.Start();

        _rb = GetComponent<Rigidbody>();

        _originalPosition = transform.position;
    }

    private void Update()
    {
        CheckPlayerHeight(_player, -_deathYPosition);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_gravityActivated)
        {
            _isPlayerOnPlatform = true;
            _gravityActivated = true;

            if (_gravityActivated)
            {
                StartCoroutine(StartShakeAfterDelay());
                StartCoroutine(PlatformFall());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayerOnPlatform = false;
        }
    }

    private IEnumerator PlatformFall()
    {
        yield return new WaitForSeconds(_fallTimer);

        _rb.isKinematic = false;
        _gravityActivated = true;

        yield return new WaitForSeconds(_shakeDuration + 1f);

        Destroy(gameObject);       
    }

    private IEnumerator StartShakeAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShakePlatform());
    }

    private IEnumerator ShakePlatform()
    {
        float shakeTimer = 0f;

        while (shakeTimer < _shakeDuration)
        {
            Vector3 randomPos = _originalPosition + Random.insideUnitSphere * _shakeIntensity;
            randomPos.y = _originalPosition.y;
            transform.position = randomPos;

            shakeTimer += Time.deltaTime;

            yield return null;
        }

        transform.position = _originalPosition;
    }
}