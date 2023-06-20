using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuNavigate : MonoBehaviour
{
    [SerializeField] GameObject _firstSelected;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _setting;
    [SerializeField] GameObject _level;

    private GameObject _lastSelected;

    public void CloseOptions()
    {

        if(_setting.activeSelf){
            _menu.SetActive(true);
            _setting.SetActive(false);
        }
        
    }
    public void CloseLevels(){
        if(_level.activeSelf){
            _level.SetActive(false);
            _menu.SetActive(true);
        }
    }
    public void OptionsOpen()
    {
        _menu.SetActive(false);
        _setting.SetActive(true);
    }
    public void OnLevelsOpen()
    {
        _level.SetActive(true);
        _menu.SetActive(false);
    }
    public void OnExit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        // _firstSelected = GameObject.Find("Start");
        // Time.timeScale = 1;
        // EventSystem.current.SetSelectedGameObject(_firstSelected);
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null && _lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(_lastSelected);
        }
        else
        {
            _lastSelected = EventSystem.current.currentSelectedGameObject;
        }

    }



}
