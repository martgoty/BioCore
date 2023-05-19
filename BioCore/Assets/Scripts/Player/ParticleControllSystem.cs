using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControllSystem : MonoBehaviour
{
    private Vector2 _stayPosition;
    private ParticleSystem _system;
    private Movement _movement;
    private bool _isWaiting = false;

    private void Start()
    {
        _system = GetComponent<ParticleSystem>();
        _movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    private void Update()
    {
        if(_movement.IsGrounded() && _isWaiting)
        {
            _stayPosition = new Vector2(_movement.transform.position.x + _movement._groundCheckCenter.x, _movement.transform.position.y + _movement._groundCheckCenter.y);
            transform.position = _stayPosition;
            _system.Play();
            _isWaiting = false;
        }
        else if(!_movement.IsGrounded() && !_isWaiting)
        {
            _isWaiting = true;
        }
    }
}
