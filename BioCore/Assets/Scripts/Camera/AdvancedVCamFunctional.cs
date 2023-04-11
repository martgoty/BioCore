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
    [SerializeField] private bool _drawGizmos;

    private GameObject _player;
    private bool _isDisableYFollow;
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
        if (_oldPlayerPositionY > _player.transform.position.y)
        {
            _oldPlayerPositionY = _player.transform.position.y;
        }
        if (_player.GetComponent<Movement>().IsRight)
        {
            _xPos = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x + _offsetActive.x, ref _xVelocity, _xSmoothDamp);
        }
        else
        {
            _xPos = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x - _offsetActive.x, ref _xVelocity, _xSmoothDamp);
        }


        if (_isDisableYFollow)
        {
            _yPos = Mathf.SmoothDamp(transform.position.y, _oldPlayerPositionY + _offsetPassive.y, ref _yVelocity, _ySmoothDamp);
        }
        else
        {
            _yPos = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y + _offsetActive.y, ref _yVelocity, _ySmoothDamp);
        }
        transform.position = new Vector3(_xPos, _yPos, transform.position.z);
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
        if (_drawGizmos)
        {
            Gizmos.color = Color.yellow;
            if (_player != null)
            {
                if (_player.GetComponent<Movement>().IsRight)
                {
                    Gizmos.DrawCube(new Vector3(_player.transform.position.x + _offsetActive.x, _player.transform.position.y + _offsetActive.y), new Vector3(0.2f, 0.2f, 0.2f));
                }
                else
                {
                    Gizmos.DrawCube(new Vector3(_player.transform.position.x - _offsetActive.x, _player.transform.position.y + _offsetActive.y), new Vector3(0.2f, 0.2f, 0.2f));
                }
            }

            Gizmos.color = Color.black;
            if (_player != null)
            {
                if (_player.GetComponent<Movement>().IsRight)
                {
                    Gizmos.DrawCube(new Vector3(_player.transform.position.x + _offsetPassive.x, _player.transform.position.y + _offsetPassive.y), new Vector3(0.2f, 0.2f, 0.2f));
                }
                else
                {
                    Gizmos.DrawCube(new Vector3(_player.transform.position.x - _offsetPassive.x, _player.transform.position.y + _offsetPassive.y), new Vector3(0.2f, 0.2f, 0.2f));
                }
            }
        }
        

    }
}
