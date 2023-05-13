using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Items Control")]
    [SerializeField] private ScrollRect _scroll;
    [SerializeField] private ItemField _itemTemplate;
    [SerializeField] private Transform _container;
    [Header("Active Swipe")]
    [SerializeField] private int _countToSwipe;
    private GameObject _currentItem;
    private int _countOfItem = 0;
    private int _currentItemIndex = 1;
    [Header("Sorting")]
    [SerializeField] ChangeActiveSortButton[] _sortTypeButtons;
    private int _currentSortType = 0;

        
    public void SwipeScroll(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        
        if (gameObject.activeSelf)
        {
            CheckActiveElementChange(dir.y);
            if (dir.y < 0)
            {
                if (_currentItemIndex < _countOfItem)
                {

                    if (_currentItemIndex - _countToSwipe > 0)
                    {
                        _scroll.verticalNormalizedPosition = 1f - (((float)_currentItemIndex - _countToSwipe) / (_countOfItem - _countToSwipe));
                    }
                }
            }
            else if (dir.y > 0)
            {
                if (_currentItemIndex > 1)
                {

                    //if (_currentItemIndex <= _countOfItem - _countToSwipe)
                    //{
                    //    _scroll.verticalNormalizedPosition = (float)(_currentItemIndex - 1) / (_countOfItem - _countToSwipe);
                    //}
                }
            }

            if (context.performed)
            {
                
            }
        }
        
    }
    public void OnChangeSort(InputAction.CallbackContext context)
    {
        float dir = context.ReadValue<float>();
        if (context.performed && gameObject.activeSelf)
        {
            if (dir > 0 && _currentSortType < _sortTypeButtons.Length - 1)
            {
                _sortTypeButtons[_currentSortType].DropActive();
                _currentSortType++;
                _sortTypeButtons[_currentSortType].SetActive();
            }
            else if(dir < 0 && _currentSortType > 0)
            {
                _sortTypeButtons[_currentSortType].DropActive();
                _currentSortType--;
                _sortTypeButtons[_currentSortType].SetActive();
            }
            UpdateItems((_currentSortType + 1).ToString());
            EventSystem.current.SetSelectedGameObject(_currentItem);
            DropScrollView();
        }

    }

    private void DropScrollView()
    {
        _scroll.verticalNormalizedPosition = 1f;
        _currentItemIndex = 1;
    }

    private void OnDisable()
    {
        _currentItem = null;
    }
    private void OnEnable()
    {
        
        SqliteDataReader reader = MyDataBase.GetReader($"SELECT * FROM Inventory WHERE type = {_currentSortType + 1}" );

        if (reader.HasRows)
        {
            TemplateInstantiate(reader);
        }

        foreach(var sortTypeButton in _sortTypeButtons)
        {
            sortTypeButton.DropActive();
        }
        _sortTypeButtons[_currentSortType].SetActive();
        EventSystem.current.SetSelectedGameObject(_currentItem);
        DropScrollView();
    }

    private void CheckActiveElementChange(float dir)
    {
        Debug.Log("Cheking");
        if(_currentItem != EventSystem.current.currentSelectedGameObject)
        {
            Debug.Log("Complete");

            _currentItem = EventSystem.current.currentSelectedGameObject;
            if(dir > 0)
            {
                _currentItemIndex--;
            }
            else if(dir < 0)
            {
                _currentItemIndex++;
            }
            
        }
    }

    private void TemplateInstantiate(SqliteDataReader reader)
    {
        _countOfItem = 0;
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        while (reader.Read())
        {
            var cell = Instantiate(_itemTemplate, _container);
            cell.LoadItems(reader);
            if(_currentItem == null)
            {
                _currentItem = cell.gameObject;
            }
            _countOfItem++;
        }
    }

    public void UpdateItems(string type)
    {
        _currentItem = null;
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
        _countOfItem = 0;
        SqliteDataReader reader = MyDataBase.GetReader($"SELECT * FROM Inventory WHERE type = {type}");
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                var cell = Instantiate(_itemTemplate, _container);
                cell.LoadItems(reader);
                if (_currentItem == null)
                {
                    _currentItem = cell.gameObject;
                }
                _countOfItem++;
            }
        }
    }

}

