using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuNavigate : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;                    //система управления
    [SerializeField] private GameObject[] _menuWindows;             //окна интерфейса
    private int numOfWindow = 0;                                    //номер активного окна
    private GameObject lastSelected;                                //предыдущий выбранный объект интерфейса

    private void Awake()
    {
        GlobalEventsSystem.OnOpenMenu.AddListener(OpenMenu);
    }



    public void OpenNewWindow(int num)
    {
        numOfWindow = num;
        _menuWindows[num].SetActive(true);
        _menuWindows[num - 1].SetActive(false);
    }

    public void CloseNewWindow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (numOfWindow == 0)
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
                _menuWindows[numOfWindow].SetActive(false);
                _menuWindows[numOfWindow - 1].SetActive(true);
                numOfWindow--;
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

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null && lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
        else
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

}
