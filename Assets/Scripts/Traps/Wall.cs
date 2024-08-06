using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP Final Camila Gonella

public class Wall : Traps
{
    [Header("Values")]
    [SerializeField] float _maxDisZ = 5.0f;
    [SerializeField] float _minDisZ = 0.0f;
    [SerializeField] float _timeToOpen;
    [SerializeField] float _timeToClose;
    [SerializeField] float _moveSpeed = 50f;
    [SerializeField] bool _prototype;
    [SerializeField] bool _level;

    private List<Wall> collidingWalls = new List<Wall>();

    Vector3 _initialPos;

    public Transform wall;

    new private void Start()
    {
        base.Start();

        if (wall == null)
        {
            enabled = false;
            return;
        }

        _initialPos = wall.position;

        StartCoroutine(OpenCloseWall());
    }

    private IEnumerator MoveWall(float fromDistance, float toDistance, float moveSpeed)
    {
        float timeElapsed = 0f;

        //nivel prototipo
        if (_prototype)
        {
            Vector3 startPos = _initialPos + Vector3.forward * fromDistance;
            Vector3 endPos = _initialPos + Vector3.forward * toDistance;

            while (timeElapsed < moveSpeed)
            {
                float t = timeElapsed / moveSpeed;
                wall.position = Vector3.Lerp(startPos, endPos, t);
                timeElapsed += Time.deltaTime * moveSpeed;
                yield return null;
            }

            wall.position = endPos;
        }

        if (_level)
        {
            Vector3 startPos = _initialPos + Vector3.right * fromDistance;
            Vector3 endPos = _initialPos + Vector3.right * toDistance;

            while (timeElapsed < moveSpeed)
            {
                float t = timeElapsed / moveSpeed;
                wall.position = Vector3.Lerp(startPos, endPos, t);
                timeElapsed += Time.deltaTime * moveSpeed;
                yield return null;
            }

            wall.position = endPos;
        }
       
    }

    private IEnumerator OpenCloseWall()
    {
        while (true)
        {
            yield return MoveWall(_minDisZ, _maxDisZ, _moveSpeed);
            yield return new WaitForSeconds(_timeToOpen);
            yield return MoveWall(_maxDisZ, _minDisZ, _moveSpeed);
            yield return new WaitForSeconds(_timeToClose);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collidingWalls.Add(this);
            CheckPlayerDeath();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collidingWalls.Remove(this);
        }
    }

    private void CheckPlayerDeath()
    {
        if (collidingWalls.Count >= 2)
        {
            base.TakeDamage(100);
            base.Die();
        }
    }

}