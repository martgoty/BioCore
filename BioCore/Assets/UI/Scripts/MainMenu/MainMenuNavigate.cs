using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuNavigate : MonoBehaviour
{
    [SerializeField] GameObject _firstSelected;
    private GameObject _lastSelected;
    public void OnEnterGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OnExit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        _firstSelected = GameObject.Find("Start");
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(_firstSelected);
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
