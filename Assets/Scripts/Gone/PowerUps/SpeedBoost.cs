using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PowerUps
{
    [SerializeField]
    float _speedBoost = 5;

    Movement _movement;

    bool _playerInRange = false;

    private void Update()
    {
        if(_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ApplyPowerUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _playerInRange = false;
    }

    public override void ApplyPowerUp()
    {
        _movement = GetComponent<Movement>();
        
        if(_movement != null)
        {
            _movement.ApplySpeedBoost(_speedBoost);
        }

        Destroy(gameObject);
    }


}
