using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    [SerializeField] private float _disableControllSeconds;

    private PlayerController _playerController;
    private PingPongAI _AIMovement;
    private Rigidbody2D _rb;
    private bool _isPlayer;
    private void Awake()
    {
        if (gameObject.CompareTag("Player"))
            _isPlayer = true;
        else
            _isPlayer = false;
    }
    private void Start()
    {
        if(_isPlayer)
        {
            _playerController = GetComponent<PlayerController>();
        }
        else
        {
            _AIMovement = GetComponent<PingPongAI>();
        }
        _rb = GetComponent<Rigidbody2D>();


    }
    public void GetDamagePlayer(Vector2 dir, float force)
    {
        StartCoroutine(DisableControll(dir,force));
    }

    IEnumerator DisableControll(Vector2 dir, float force)
    {
        if (_isPlayer)
        {
            _playerController.enabled = false;
            _rb.velocity = dir * force;
            yield return new WaitForSeconds(_disableControllSeconds);
            _playerController.enabled = true;
            GlobalEventsSystem.TakeDamage();

        }
        else
        {
            _rb.velocity = dir * force;
            yield return new WaitForSeconds(_disableControllSeconds);
        }

    }
}
