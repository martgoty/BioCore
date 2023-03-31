using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Vector2 _attackPointUp;
    [SerializeField] private Vector2 _attackPointDown;
    [SerializeField] private Vector2 _attackPointRight;
    [SerializeField] private Vector2 _attackPointLeft;
    [SerializeField] private float _attackRadius;

    [HideInInspector] public AttackDirections attackState;


    private bool isStart = false;
    private Collider2D[] hit;
    private void Start()
    {
        isStart = true;
    }

    public void Attack()
    {
        switch (attackState)
        {
            case AttackDirections.Left:
                hit = Physics2D.OverlapCircleAll(_attackPointLeft +  new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
            case AttackDirections.Right:
                hit = Physics2D.OverlapCircleAll(_attackPointRight + new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer, _attackRadius, _enemyLayer);
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
            switch (attackState)
            {
                case AttackDirections.Left:
                    Gizmos.DrawWireSphere(_attackPointLeft +  new Vector2(transform.position.x, transform.position.y), _attackRadius);
                    break;
                case AttackDirections.Right:
                    Gizmos.DrawWireSphere(_attackPointRight + new Vector2(transform.position.x, transform.position.y), _attackRadius);
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
            Gizmos.DrawWireSphere(_attackPointLeft + new Vector2(transform.position.x, transform.position.y), _attackRadius);
            Gizmos.DrawWireSphere(_attackPointRight + new Vector2(transform.position.x, transform.position.y), _attackRadius);
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
