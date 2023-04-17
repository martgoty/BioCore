using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triger02 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GlobalEventsSystem.Score();
            Destroy(gameObject);
        }
    }
}
