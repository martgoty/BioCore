using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform _point;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Debug.Log("Detected");
            other.transform.position =_point.position;
        }
    }
}
