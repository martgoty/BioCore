using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResizeCamera : MonoBehaviour
{
    [SerializeField] float _minSize = 5f;
    [SerializeField] float _maxSize = 10f;
    [SerializeField] float _speedResize = 4f;
    private CinemachineVirtualCamera _vCamera;
    private bool _isResizing = false;


    private void Awake()
    {
        _vCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void StartResize()
    {
        _isResizing = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (_isResizing)
        {
            if(_vCamera.m_Lens.OrthographicSize < _maxSize)
            {
                _vCamera.m_Lens.OrthographicSize += _speedResize * Time.deltaTime;
            }
            else
            {
                GetComponent<ResizeCamera>().enabled = false;
            }
        }
    }
}
