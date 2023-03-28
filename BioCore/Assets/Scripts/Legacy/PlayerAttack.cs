using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Vector2 _attackPointUp;
    [SerializeField] private Vector2 _attackPointDown;
    [SerializeField] private Vector2 _attackPointHorizontal;
    [SerializeField] private float _attackRadius;

    [HideInInspector]public AttackDirections _attackState;


    private bool isStart = false;
    private Collider2D[] hit;
    private void Start()
    {
        isStart = true;
    }

    public void Attack()
    {
        switch (_attackState)
        {
            case AttackDirections.Left:
                hit = Physics2D.OverlapCircleAll(_attackPointHorizontal +  new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
            case AttackDirections.Right:
                hit = Physics2D.OverlapCircleAll(new Vector2(_attackPointHorizontal.x * (-1) + transform.position.x, _attackPointHorizontal.y + transform.position.y), _attackRadius, _enemyLayer);
                break;
            case AttackDirections.Down:
                hit = Physics2D.OverlapCircleAll(_attackPointDown + new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
            case AttackDirections.Up:
                hit = Physics2D.OverlapCircleAll(_attackPointUp + new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
        }


        if (hit != null)
        {
            for(int i = 0; i < hit.Length; i++)
            {
                hit[i].gameObject.SetActive(false);  
            }

        }



    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (isStart)
        {
            switch (_attackState)
            {
                case AttackDirections.Left:
                    Gizmos.DrawWireSphere(_attackPointHorizontal + new Vector2(transform.position.x, transform.position.y), _attackRadius);
                    break;
                case AttackDirections.Right:
                    Gizmos.DrawWireSphere(new Vector2(_attackPointHorizontal.x * (-1) + transform.position.x, _attackPointHorizontal.y + transform.position.y), _attackRadius);
                    break;
                case AttackDirections.Down:
                    Gizmos.DrawWireSphere(_attackPointDown + new Vector2(transform.position.x, transform.position.y), _attackRadius);
                    break;
                case AttackDirections.Up:
                    Gizmos.DrawWireSphere(_attackPointUp + new Vector2(transform.position.x, transform.position.y), _attackRadius);
                    break;
            }
        }
        else
        {
            Gizmos.DrawWireSphere(_attackPointHorizontal + new Vector2(transform.position.x, transform.position.y), _attackRadius);
            Gizmos.DrawWireSphere(new Vector2(_attackPointHorizontal.x * (-1) + transform.position.x, _attackPointHorizontal.y + transform.position.y), _attackRadius);
            Gizmos.DrawWireSphere(_attackPointDown + new Vector2(transform.position.x, transform.position.y), _attackRadius);
            Gizmos.DrawWireSphere(_attackPointUp + new Vector2(transform.position.x, transform.position.y), _attackRadius);

        }
    }

    public enum AttackDirections
    {
        Left,
        Right,
        Up,
        Down
    }
}
