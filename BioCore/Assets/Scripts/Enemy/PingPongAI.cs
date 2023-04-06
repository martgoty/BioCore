using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class PingPongAI : MonoBehaviour
{
    [Header("Directional Ray")]
    [SerializeField] private float _dirRayDistance;
    [SerializeField] private float _flipDistance;
    [SerializeField] private Vector2 _dirRayOffset;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private string _animatorParametrsName;
    private bool _isRight;

    private SpriteRenderer _spriteRenderer;
    private EnemyMovement _movement;
    private Animator _animator;
    private RaycastHit2D wallCheckHit;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<EnemyMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (_isRight)
        {
            wallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x + _dirRayOffset.x, transform.position.y + _dirRayOffset.y), Vector2.right, _dirRayDistance, _obstacleLayer);
        }
        else
        {
            wallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x - _dirRayOffset.x, transform.position.y + _dirRayOffset.y), Vector2.left, _dirRayDistance, _obstacleLayer);
        }
        //Смена направление при достижении расстояние поворота
        if (wallCheckHit.collider != null)
        {
            var currDist = Mathf.Abs(transform.position.x - wallCheckHit.point.x);
            if (currDist <= _flipDistance)
            {
                _isRight = !_isRight;
            }
        }

        if (_isRight)
        {
            _spriteRenderer.flipX = true;
            _animator.SetBool(_animatorParametrsName, true);
        }
        else
        {
            _spriteRenderer.flipX = false;
            _animator.SetBool(_animatorParametrsName, false);

        }
    }

    private void FixedUpdate()
    {
        if (_isRight)
        {
            _movement.Move(Vector2.right, false);
        }
        else
        {
            _movement.Move(Vector2.left, false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isRight)
        {
            Gizmos.DrawRay(new Vector2(transform.position.x + _dirRayOffset.x, transform.position.y + _dirRayOffset.y), Vector2.right * _dirRayDistance);
        }
        else
        {
            Gizmos.DrawRay(new Vector2(transform.position.x - _dirRayOffset.x, transform.position.y + _dirRayOffset.y), Vector2.left * _dirRayDistance);
        }
    }
}

