using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfomsMove : MonoBehaviour
{
    private Movement player;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player") && player._isGrounded)
        {
            other.transform.parent = transform;
        }

    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player") || !player._isGrounded)
        {
            other.transform.parent = null;
        }   
    }
}
