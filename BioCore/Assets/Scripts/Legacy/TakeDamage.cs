using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private float _force;

    private PlayerController _playerController;
    private Rigidbody2D _rigidbody2D;
    private HP _hp;
    private Vector2 direction;
    private bool isForce = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GlobalEventsManager.SendPlayerDamage();
            direction = (transform.position - collision.transform.position);
            direction = new Vector2(direction.x + 0.2f, direction.y + 0.2f);
            direction = direction.normalized;
            StartCoroutine(DisableControll());
            _hp.ReduceHealthPoint();

        }

    }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _hp = GetComponent<HP>();
    }

    private void FixedUpdate()
    {
        if (isForce)
        {
            _rigidbody2D.AddForce(direction * _force, ForceMode2D.Impulse);
            isForce = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, direction);
    }

    IEnumerator DisableControll()
    {
        _rigidbody2D.velocity = Vector2.zero;
        isForce = true;
        _playerController.enabled = false;
        yield return new WaitForSeconds(_timer);
        _playerController.enabled = true;
    }
}
