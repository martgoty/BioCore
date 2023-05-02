using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDamageToPlayer : MonoBehaviour
{
    private Vector2 _outDir;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                _outDir = new Vector2(-1f, 0.6f);
            }
            else
            {
                _outDir = new Vector2(1f, 0.6f);
            }
            GlobalEventsSystem.PlayerTakeDamage(_outDir);
        }
    }


}
