using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ActionsWindow : MonoBehaviour
{
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;

    public void EnableButton(InputAction.CallbackContext context)
    {
        if(context.canceled && gameObject.activeSelf)
        {
            _button1.interactable = true;
            _button2.interactable = true;

        }
    }

    private void OnDisable()
    {
        _button1.interactable = false;
        _button2.interactable = false;
    }
}
