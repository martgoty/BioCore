using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AdvancedVCamFunctional : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetActive;
    [SerializeField] private Vector3 _offsetPassive;
    [SerializeField, Range(0,1)] private float _xSmoothDamp;
    [SerializeField, Range(0,1)] private float _ySmoothDamp;
    [SerializeField] private bool _isDisableYFollow;
    private GameObject _player;
    private float _oldPlayerPositionY;
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
            _xPos = Mathf.SmoothDamp(transform.position.x + _offsetActive.x, _player.transform.position.x + _offsetActive.x, ref _xVelocity, _xSmoothDamp);

            if (_isDisableYFollow)
                _yPos = Mathf.SmoothDamp(transform.position.y + _offsetPassive.y, _oldPlayerPositionY + _offsetPassive.y, ref _yVelocity, _ySmoothDamp);
            else
                _yPos = Mathf.SmoothDamp(transform.position.y + _offsetActive.y, _player.transform.position.y + _offsetActive.y, ref _yVelocity, _ySmoothDamp);
            transform.position = new Vector3(_xPos, _yPos, transform.position.z);
        }
        else
        {
            _xPos = Mathf.SmoothDamp(transform.position.x + _offsetActive.x , _player.transform.position.x - _offsetActive.x, ref _xVelocity, _xSmoothDamp);

            if (_isDisableYFollow)
                _yPos = Mathf.SmoothDamp(transform.position.y + _offsetPassive.x, _oldPlayerPositionY + _offsetPassive.y, ref _yVelocity, _ySmoothDamp);
            else
                _yPos = Mathf.SmoothDamp(transform.position.y + _offsetActive.x , _player.transform.position.y + _offsetActive.y, ref _yVelocity, _ySmoothDamp);

            transform.position = new Vector3(_xPos, _yPos, transform.position.z);
        }
    }

    public void DisableYFollow()
    {
        _isDisableYFollow = true;
        _oldPlayerPositionY = _player.transform.position.y;
    }

    public void EnableYFollow()
    {
        _isDisableYFollow = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position + _offsetActive, new Vector3(0.2f, 0.2f, 0.2f));

        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position + _offsetPassive, new Vector3(0.2f, 0.2f, 0.2f));

    }
}
