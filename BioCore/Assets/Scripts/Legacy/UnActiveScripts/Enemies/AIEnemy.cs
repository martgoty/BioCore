using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EnemyMovement))]
public class AIEnemy : MonoBehaviour
{
    [Header("Directional Ray")]
    [SerializeField] private float _dirRayDistance;
    [SerializeField] private float _flipDistance;
    [SerializeField] private Vector2 _dirRayOffset;
    [SerializeField] private LayerMask _obstacleLayer;
    public bool _isRight;

    [Header("Attack Ray")]
    [SerializeField] private float _detectRayDistance;
    [SerializeField] private float _attackDistance;
    [SerializeField] private Vector2 _detectRayOffset;

    private SpriteRenderer _spriteRenderer;
    private EnemyMovement _movement;
    private RaycastHit2D wallCheckHit;
    private RaycastHit2D forwardHit;
    private RaycastHit2D backHit;
    private State _stateEnemy;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<EnemyMovement>();
    }

    /// <summary>
    /// ����� Walking ���������� ���� ��������� �� ��������� ������
    /// </summary>
    private void Walking()
    {
        //�������� ���� � ����������� �����������
        if (_isRight)
        {
            wallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x + _dirRayOffset.x, transform.position.y + _dirRayOffset.y), Vector2.right, _detectRayDistance, _obstacleLayer);
        }
        else
        {
            wallCheckHit = Physics2D.Raycast(new Vector2(transform.position.x - _dirRayOffset.x, transform.position.y + _dirRayOffset.y), Vector2.left, _detectRayDistance, _obstacleLayer);
        }
        //����� ����������� ��� ���������� ���������� ��������
        if (wallCheckHit.collider != null)
        {
            var currDist = Mathf.Abs(transform.position.x - wallCheckHit.point.x);
            if (currDist <= _flipDistance)
            {
                _isRight = !_isRight;
            }
        }
    }

    /// <summary>
    /// ����� Detecting ������������ ������ � ����������� ������ � ����� �������������
    /// </summary>
    private void Detecting()
    {
        //�������� ����� � ����������� �����������
        if (_isRight)
        {
            forwardHit = Physics2D.Raycast(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right, _detectRayDistance, _obstacleLayer);
            backHit = Physics2D.Raycast(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left, _detectRayDistance, _obstacleLayer);
        }
        else
        {
            forwardHit = Physics2D.Raycast(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left, _detectRayDistance, _obstacleLayer);
            backHit = Physics2D.Raycast(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right, _detectRayDistance, _obstacleLayer);
        }
        //����� ����������� ��� ���������� ������, ���� �� �� ������ 
        if(backHit.collider != null)
        {
            if (backHit.collider.CompareTag("Player"))
            {
                _isRight = !_isRight;
            }
        }
        //������� � ����� ������������� ��� ����������� �����
        else if(forwardHit.collider != null)
        {
            if (forwardHit.collider.CompareTag("Player"))
            {
                _stateEnemy = State.chase;
            }
        }
    }
    /// <summary>
    /// ����� Chasing �������� ���� ����� ��������� � ���� ������ �������
    /// </summary>
    private void Chasing()
    {
        //�������� ����� � ����������� �����������
        if (_isRight)
        {
            forwardHit = Physics2D.Raycast(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right, _detectRayDistance, _obstacleLayer);
            backHit = Physics2D.Raycast(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left, _detectRayDistance, _obstacleLayer);
        }
        else
        {
            forwardHit = Physics2D.Raycast(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left, _detectRayDistance, _obstacleLayer);
            backHit = Physics2D.Raycast(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right, _detectRayDistance, _obstacleLayer);
        }
        
        if (backHit.collider != null && backHit.collider.CompareTag("Player"))//����� ����������� ��� ���������� ������, ���� �� �� ������ 
        {
            _isRight = !_isRight;
            
        }
        else if(forwardHit.collider != null && forwardHit.collider.CompareTag("Player"))//�����, ���� ����� ��������� �� ����������� ����������
        {
            var currDist = Mathf.Abs(transform.position.x - forwardHit.point.x);
            if (currDist <= _attackDistance)
            {

                _stateEnemy = State.attack;
            }
        }
        else//����������� � ����� ������, ���� ����� ���� �� ���� ������
        {
            _stateEnemy = State.walk;
        }


    }

    private void Attacking()
    {
        //�������� ����� � ����������� �����������
        if (_isRight)
        {
            forwardHit = Physics2D.Raycast(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right, _detectRayDistance, _obstacleLayer);
            backHit = Physics2D.Raycast(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left, _detectRayDistance, _obstacleLayer);
        }
        else
        {
            forwardHit = Physics2D.Raycast(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left, _detectRayDistance, _obstacleLayer);
            backHit = Physics2D.Raycast(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right, _detectRayDistance, _obstacleLayer);
        }

        if (backHit.collider != null && backHit.collider.CompareTag("Player"))//����� ����������� ��� ���������� ������, ���� �� �� ������ 
        {
            _isRight = !_isRight;

        }
        else if (forwardHit.collider != null && forwardHit.collider.CompareTag("Player"))//�����, ���� ����� ��������� �� ����������� ����������
        {
            var currDist = Mathf.Abs(transform.position.x - forwardHit.point.x);
            if (currDist <= _attackDistance)
            {
                Debug.Log("Attack");
            }
            else
            {
                _stateEnemy = State.chase;
            }
        }
        else//����������� � ����� ������, ���� ����� ���� �� ���� ������
        {
            _stateEnemy = State.walk;
        }
    }

    private void Update()
    {
        switch (_stateEnemy)
        {
            case State.walk:
                Walking();
                Detecting();
                break;
            case State.chase:
                Chasing();
                break;
            case State.attack:
                Attacking();
                break;
        }

        if (_isRight)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        switch (_stateEnemy)
        {
            case State.walk:
                if (_isRight)
                {
                    _movement.Move(Vector2.right, false);
                }
                else
                {
                    _movement.Move(Vector2.left, false);
                }
                break;
            case State.chase:
                if (_isRight)
                {
                    _movement.Move(Vector2.right, true);
                }
                else
                {
                    _movement.Move(Vector2.left, true);
                }
                break;
            case State.attack:
                _movement.Move(Vector2.zero, true);
                break;
        }
    }

    private enum State
    {
        walk,
        chase,
        attack
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

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right * _detectRayDistance);
        Gizmos.DrawRay(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left * _detectRayDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector2(transform.position.x + _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.right * _attackDistance);
        Gizmos.DrawRay(new Vector2(transform.position.x - _detectRayOffset.x, transform.position.y + _detectRayOffset.y), Vector2.left * _attackDistance);
    }
}
