using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSuperDuperPuper : MonoBehaviour
{
    [SerializeField] private Transform _pointToTele;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _pointToTele.position;
        }
    }
}
