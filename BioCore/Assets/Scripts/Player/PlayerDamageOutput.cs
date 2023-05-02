using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageOutput : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _timeDisable;
    private PlayerController _playerController;
    private Rigidbody2D _rb;
    private bool _canTakeDamage = true;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();

        GlobalEventsSystem.OnPlayerTakeDamage.AddListener(GetOutput);
    }

    private void GetOutput(Vector2 outDir)
    {
        if (_canTakeDamage)
        {
            StartCoroutine(DisableControll(outDir, _force, _timeDisable));
        }
    }

    //Отключение управления, чтобы оно не обнуляло скорость толчка
    public IEnumerator DisableControll(Vector2 outDir, float force, float time)
    {
        _playerController.enabled = false;
        _rb.velocity = outDir * force;
        _canTakeDamage = false;
        yield return new WaitForSeconds(time);
        _playerController.enabled = true;
        _canTakeDamage = true;
    }
}
