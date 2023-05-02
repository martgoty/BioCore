//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SetDamage : MonoBehaviour
//{
//    [SerializeField] private Vector2 _direction;
//    [SerializeField] private float _force;
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            Vector2 dir;
//            if (transform.position.x > collision.transform.position.x)
//            {
//                dir = new Vector2(_direction.x * -1, _direction.y);
//            }
//            else
//            {
//                dir = _direction;
//            }
//            collision.gameObject.GetComponent<GetDamage>().GetDamagePlayer(dir, _force);
//        }


//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.blue;
//        Gizmos.DrawRay(transform.position, _direction);
//    }
//}
