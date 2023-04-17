using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentDamage : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _point;
    [SerializeField] private RawImage _image;
    [SerializeField] private float _time;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GlobalEventsSystem.TakeDamage();
            StartCoroutine(daley());
        }


    }

    IEnumerator daley()
    {

        _image.color = new Vector4(_image.color.r, _image.color.g, _image.color.b, 255);
        _player.transform.position = _point.position;
        yield return new WaitForSeconds(_time);
        _image.color = new Vector4(_image.color.r, _image.color.g, _image.color.b, 0);

    }
}
