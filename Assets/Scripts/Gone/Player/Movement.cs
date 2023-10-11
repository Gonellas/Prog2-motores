using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    [SerializeField]
    float _speed = 5;
    //float _speed;
    //Transform _transform;

    //public Movement(Transform transform, float speed = 2)
    //{
    //    _transform = transform;
    //    _speed = speed;
    //}

    //public void Move(float vertical, float horizontal)
    //{
    //    var dir = _transform.forward * vertical;
    //    dir += _transform.right * horizontal;

    //    _transform.position += dir.normalized * _speed * Time.deltaTime;
    //}

    //public void Jump()
    //{
    //    Debug.Log("SALTE");
    //}

    public void ApplySpeedBoost(float boost)
    {
        _speed += boost;
    }
}
