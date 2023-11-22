using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Transform wall;

    SceneManagerController sceneManagerController;

   [SerializeField]
    float _maxDisZ = 5.0f;

    [SerializeField]
    float _minDisZ = 0.0f;

    [SerializeField]
    [Range(0, 10)]
    float _wallSpeed = 1.0f;

    PlayerHealth playerHealth;

    [SerializeField]
    private List<Wall> collidingWalls = new List<Wall>();

    Vector3 _initialPos;

    bool _isMoved = false;

    private void Start()
    {
        sceneManagerController = GetComponent<SceneManagerController>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collidingWalls.Add(this);
            CheckPlayerDeath();
        }       
    }

    private void CheckPlayerDeath()
    {
        if (collidingWalls.Count == 2)
        {
            Invoke("RestartLevel", 3.0f);
        }
    }

    private void RestartLevel()
    {
        sceneManagerController.RestartLevel();
    }

}