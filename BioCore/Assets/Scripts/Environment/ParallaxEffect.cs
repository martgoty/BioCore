using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField, Range(-1f, 1f)] private float _parallaxSpeed;
    [SerializeField] private bool _disableYParallax;
    private Transform _camera;
    private Vector3 _position;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }
    private void Start()
    {
        _position = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = _camera.position - _position;

        if (_disableYParallax)
            delta.y = 0;

        _position = _camera.position;

        transform.position += delta * _parallaxSpeed;
    }


}
