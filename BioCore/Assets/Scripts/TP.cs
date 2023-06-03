using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TP : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GlobalEventsSystem.TakeDamage();
            collision.transform.position = _target.position;
        }
    }



}
