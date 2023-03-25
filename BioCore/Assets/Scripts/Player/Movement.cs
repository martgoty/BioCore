using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    [Header("Player Movement")]
    [Space]
    [SerializeField] private float _moveSpeed;//максимальная скорость движения
    [SerializeField] private float _jumpForce;//сила прыжка
    [SerializeField] private float _jumpDeceleration;//резкость динамического прыжка
    [SerializeField] private float _maxFallSpeed;//максимальная скорость падения
    [SerializeField] private bool _isRight;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    [Header("Ground Check")]
    [Space]
    [SerializeField][Range(0f, 1f)] private float _checkSphereRadius;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _checkSphereCenter;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void PlayerMove(float horizontalMove, bool jump, bool jumpStop)
    {
        //ограничение максимальной скорости падения
        if (_rigidbody2D.velocity.y < -_maxFallSpeed)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_maxFallSpeed);
        }

        //движение игрока 
        _rigidbody2D.velocity = new Vector2(horizontalMove * 100f * _moveSpeed, _rigidbody2D.velocity.y);

        //прыжок игрока
        if (jump && IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce * 4f);
        }
        else if (jumpStop)//условие для динамичной высоты прыжка
        {
            jumpStop = false;
            if (_rigidbody2D.velocity.y > 0)
            {
                if (_jumpDeceleration > 0)
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y / _jumpDeceleration);
                }
                else
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
                }
            }
        }
    }

    public void Flip(float horizontal)
    {
        if (horizontal < 0f)
            _isRight = false;
        else if (horizontal > 0f)
            _isRight = true;

        if (_isRight)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x + _checkSphereCenter.x, transform.position.y + _checkSphereCenter.y), _checkSphereRadius, _groundLayer);
    }
    private void OnDrawGizmos()
    {
        if (IsGrounded())
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + _checkSphereCenter.x, transform.position.y + _checkSphereCenter.y), _checkSphereRadius);
    }
}
