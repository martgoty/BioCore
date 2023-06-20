using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed;//˜˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜
    [SerializeField] private float _jumpForce;//˜˜˜˜ ˜˜˜˜˜˜
    [SerializeField] private float _jumpDeceleration;//˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜
    [SerializeField] private float _maxFallSpeed;//˜˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜
    [SerializeField] private PhysicsMaterial2D[] _materials;
    private Rigidbody2D _rigidbody2D;
    public bool _isGrounded;

    [Header("Ground & Wall Check")]
    [SerializeField][Range(0f, 1f)] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] public Vector2 _groundCheckCenter;
    [SerializeField][Range(0f, 1f)] private float _wallCheckRadius;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private Vector2 _wallCheckCenter;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        _isGrounded = IsGrounded();

        if (_isGrounded)
        {
            GetComponent<CapsuleCollider2D>().sharedMaterial = _materials[0];
        }
        else
        {
            GetComponent<CapsuleCollider2D>().sharedMaterial = _materials[1];
        }

    }

    public void PlayerMove(float horizontalMove, bool jump, bool jumpStop)
    {
        //˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜
        if (_rigidbody2D.velocity.y < -_maxFallSpeed)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_maxFallSpeed);
        }

        //˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜ 
        _rigidbody2D.velocity = new Vector2(horizontalMove * 100f * _moveSpeed, _rigidbody2D.velocity.y);

        //˜˜˜˜˜˜ ˜˜˜˜˜˜
        if (jump && _isGrounded)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce * 4f);
        }
        else if (jumpStop)//˜˜˜˜˜˜˜ ˜˜˜ ˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜ ˜˜˜˜˜˜
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

        if (horizontalMove > 0f)//˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜ ˜˜˜˜˜
        {
            _wallCheckCenter = new Vector2(Mathf.Abs(_wallCheckCenter.x), _wallCheckCenter.y);
        }
        else if (horizontalMove < 0f)
        {
            if (_wallCheckCenter.x > 0)
                _wallCheckCenter = new Vector2(_wallCheckCenter.x * -1f, _wallCheckCenter.y);
        }
    }

   

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x + _wallCheckCenter.x, transform.position.y + _wallCheckCenter.y), _wallCheckRadius, _wallLayer);
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x + _groundCheckCenter.x, transform.position.y + _groundCheckCenter.y), _groundCheckRadius, _groundLayer);
    }
    private void OnDrawGizmos()
    {
        if (IsGrounded())
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + _groundCheckCenter.x, transform.position.y + _groundCheckCenter.y), _groundCheckRadius);

        if(IsWalled())
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(new Vector2(transform.position.x + _wallCheckCenter.x, transform.position.y + _wallCheckCenter.y), _wallCheckRadius);
    }
}
