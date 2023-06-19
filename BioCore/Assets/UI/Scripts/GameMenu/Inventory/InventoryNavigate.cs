using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryNavigate : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private PlayerInput _input;                    //система управления

    
    public void InventoryOpen(InputAction.CallbackContext context){
        _inventory.SetActive(true);
        Time.timeScale = 0f;
        _input.SwitchCurrentActionMap("UI");

    }

    public void CloseInventory(InputAction.CallbackContext context){
        if(_inventory.activeSelf){
            _inventory.SetActive(false);
            Time.timeScale = 1f;
            _input.SwitchCurrentActionMap("Main");

        }
    }


}
