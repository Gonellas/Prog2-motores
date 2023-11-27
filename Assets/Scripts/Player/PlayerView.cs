using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";
    

    private void Start()
    {
        if(_playerAnim == null)
        {
            _playerAnim.GetComponentInChildren<Animator>();
        }
    }

    public void SetMovement(float xAxis, float zAxis)
    {
        _playerAnim.SetFloat(_xAxisName, xAxis);
        _playerAnim.SetFloat(_zAxisName, zAxis);
    }

    public void SetJumping(bool isJumping)
    {
        _playerAnim.SetTrigger("wantsToJump");
    }
}
