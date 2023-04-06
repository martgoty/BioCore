using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AdvancedVCamFunctional : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField, Range(0,1)] private float _xSmoothDamp;
    [SerializeField, Range(0,1)] private float _ySmoothDamp;
    private GameObject _player;
    [SerializeField] private bool _isDisableYFollow;

    private float _xVelocity;
    private float _yVelocity;

    private float _xPos;
    private float _yPos;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (_player.GetComponent<Movement>().IsRight)
        {
            _xPos = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x + _offset.x, ref _xVelocity, _xSmoothDamp);
            if(_isDisableYFollow)
                _yPos = Mathf.SmoothDamp(transform.position.y, transform.position.y, ref _yVelocity, _ySmoothDamp);
            else
                _yPos = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y + _offset.y, ref _yVelocity, _ySmoothDamp);
            transform.position = new Vector3(_xPos, _yPos, transform.position.z);
        }
        else
        {
            _xPos = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x - _offset.x, ref _xVelocity, _xSmoothDamp);
            if (_isDisableYFollow)
                _yPos = Mathf.SmoothDamp(transform.position.y, transform.position.y, ref _yVelocity, _ySmoothDamp);
            else
                _yPos = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y + _offset.y, ref _yVelocity, _ySmoothDamp);
            transform.position = new Vector3(_xPos, _yPos, transform.position.z);
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));
    }
}
