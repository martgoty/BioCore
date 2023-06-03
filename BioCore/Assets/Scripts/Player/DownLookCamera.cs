using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DownLookCamera : MonoBehaviour
{
    [SerializeField] private float _secondsToLook;
    [SerializeField] private Transform _target;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _speedOfLook;
    private float dir;
    private bool isPressed = true;
    private bool canMoved = false;
    
    public void OnLookDown(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<float>();
        if (dir < 0)
        {
            if (isPressed)
            {
                StartCoroutine(DownLook());
                isPressed = false;
            }
        }
        else
        {
            isPressed = true;
            canMoved = false;
        }
    }
    private void Update()
    {
        if (canMoved)
        {
            _target.position = Vector2.Lerp(_target.position, new Vector2(transform.position.x, transform.position.y - _yOffset), Time.deltaTime * _speedOfLook);
        }
        else
        {
            _target.position = Vector2.Lerp(_target.position, transform.position, Time.deltaTime * _speedOfLook);

        }
    }
    private IEnumerator DownLook()
    {
        yield return new WaitForSeconds(_secondsToLook);
        if(dir < 0 && !isPressed)
        {
            canMoved = true;
        }
    }
}
