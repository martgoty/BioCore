using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuNavigate : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private GameObject[] _menuWindows;
    private int number = 0;

    private void Awake()
    {
        GlobalEventsSystem.OnOpenMenu.AddListener(OpenMenu);
    }

    public void OpenNewWindow(int num)
    {
        number = num;
        _menuWindows[num].SetActive(true);
        _menuWindows[num - 1].SetActive(false);
    }

    public void CloseNewWindow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (number == 0)
            {
                _input.SwitchCurrentActionMap("Main");

                foreach (var window in _menuWindows)
                {
                    window.SetActive(false);
                }

                Time.timeScale = 1f;
            }
            else
            {
                _menuWindows[number].SetActive(false);
                _menuWindows[number - 1].SetActive(true);
                number--;
            }
        }
        
    }

    private void OpenMenu()
    {
        _menuWindows[0].SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseMenu(InputAction.CallbackContext context)
    {
        _input.SwitchCurrentActionMap("Main");

        foreach (var window in _menuWindows)
        {
            window.SetActive(false);
        }

        Time.timeScale = 1f;
    }
}
