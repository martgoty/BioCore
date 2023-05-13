using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField][Range(0f,20f)] private float _walkSpeed;   //скорость хотьбы противника
    [SerializeField][Range(0f,20f)] private float _runSpeed;    //скорость бега противника
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, bool isRun)
    {
        if (isRun)
        {
            _rigidbody2D.velocity = new Vector2(direction.x * _runSpeed, _rigidbody2D.velocity.y);

        }
        else
        {
            _rigidbody2D.velocity = new Vector2(direction.x * _walkSpeed, _rigidbody2D.velocity.y);
        }

    }



}
