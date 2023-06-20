using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsDamage : MonoBehaviour
{
    [SerializeField] private Checkpoints points;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            points.TeleportPlayer();
            GlobalEventsSystem.TakeDamage();
        }
    }
}
