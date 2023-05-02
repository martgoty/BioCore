using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float _disableControllSecond;
    [SerializeField] private float _force;
    [SerializeField] private float _forceLimit;

    private bool _isEnabled = true;
    private PlayerController _playerController;
    private Rigidbody2D _rb;
    private Vector2 direction;

    private bool isPlayer = false;

    private void Start()
    {
        if(gameObject.CompareTag("Player"))
            isPlayer = true;
        else
            isPlayer = false;
    }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer)
        {
            if (collision.CompareTag("Enemy") && _isEnabled)
            {
                _isEnabled = false;
                if (transform.position.x < collision.transform.position.x)
                {
                    direction = new Vector2(-1, 0.5f);
                }
                else
                {
                    direction = new Vector2(1, 1);
                }
                StartCoroutine(DisableControllerPlayer());
            }
        }
        else
        {
            if(collision.CompareTag("Player") && _isEnabled)
            {
                _isEnabled = false;
                if (transform.position.x < collision.transform.position.x)
                {
                    direction = new Vector2(-1, 0.5f);
                }
                else
                {
                    direction = new Vector2(1, 1);
                }
            }
        }

        
    }

    IEnumerator DisableControllEnemy()
    {
        _rb.velocity = new Vector2(direction.x * _force, direction.y * _force);
        yield return new WaitForSeconds(_disableControllSecond);
        _isEnabled = true;
    }
    IEnumerator DisableControllerPlayer()
    {
        _playerController.enabled = false;
        _rb.velocity = new Vector2(direction.x * _force, direction.y * _force);
        yield return new WaitForSeconds(_disableControllSecond);
        _isEnabled = true;
        _playerController.enabled = true;
    }
}
