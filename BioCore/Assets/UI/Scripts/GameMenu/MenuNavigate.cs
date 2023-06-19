using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuNavigate : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;                    //система управления
    [SerializeField] private GameObject[] _menuWindows;             //окна интерфейса
    private GameObject lastSelected;                                //предыдущий выбранный объект интерфейс

    private void Awake()
    {
        GlobalEventsSystem.OnOpenMenu.AddListener(OpenMenu);
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

    public void Countinue(){
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

    public void VolumeSetting(){

        foreach(GameObject menus in _menuWindows){
            menus.SetActive(false);
        }
        _menuWindows[3].SetActive(true);
    }

    public void OnMenuBack(InputAction.CallbackContext context){
        if(_menuWindows[3].activeSelf){
            _menuWindows[3].SetActive(false);
            _menuWindows[0].SetActive(true);
        }
    }
}
