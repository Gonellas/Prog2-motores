using System.Collections;
using UnityEngine;

public class Wall : Traps
{
    public Transform wall;

    [SerializeField]
    float _maxDisZ = 5.0f;

    [SerializeField]
    float _minDisZ = 0.0f;

    [SerializeField]
    [Range(0, 10)]
    float _wallSpeed = 1.0f;

    PlayerHealth playerHealth;


    Vector3 _initialPos;

    bool _isMoved = false;

    private void Start()
    { 
        playerHealth = GetComponent<PlayerHealth>();

        if (wall == null)
        {
            enabled = false;
            return;
        }

        _initialPos = wall.position;
        StartCoroutine(MoveWall());
    }

    private IEnumerator MoveWall()
    {
        while (!_isMoved)
        {
            float time = Mathf.PingPong(Time.time * _wallSpeed, 1);
            wall.position = Vector3.Lerp(_initialPos + Vector3.forward * _minDisZ, _initialPos + Vector3.forward * _maxDisZ, time);

            //Cuando llega a la _maxDisZ paran de moverse
            if (time >= 0.99f) 
            {
                _isMoved = true;
            }

            yield return null;
        }
    }

    public override void TakeDamage(int damage)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(100);
        }
    }
}