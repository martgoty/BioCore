using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private MenuNavigate _navigate;
    [SerializeField] private GameObject[] _visualItems;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private GameObject _actionWindow;
    [SerializeField] private TextMeshProUGUI _title;
    private List<Item> _itemsList = new List<Item>();
    private List<string> _titleOfType = new List<string>();
    private int _currentSortType = 0;
    private int _currentOrderItem = 0;

    private bool isNavigateButtonPressed = false;

    private void OnEnable()
    {
        UpdateList();
        ChangeList();
        UpdateTitle();
    }

    public void ChangeSortType(InputAction.CallbackContext context)
    {
        float dir = context.ReadValue<float>();
        if (context.performed && gameObject.activeSelf)
        {
            if (dir > 0)
            {
                if (_currentSortType < _titleOfType.Count - 1)
                {
                    _currentSortType++;
                    if (_currentSortType == _titleOfType.Count - 1)
                    {
                        _panels[1].SetActive(false);
                    }
                    else
                    {
                        _panels[0].SetActive(true);
                    }
                }
            }
            else if (dir < 0)
            {
                if (_currentSortType > 0)
                {
                    _currentSortType--;
                    if (_currentSortType == 0)
                    {
                        _panels[0].SetActive(false);
                    }
                    else
                    {
                        _panels[1].SetActive(true);
                    }
                }
            }
            if(_currentSortType >= 0 && _currentSortType < _titleOfType.Count)
            {
                _currentOrderItem = 0;
                _title.text = _titleOfType[_currentSortType];
                UpdateList();
                ChangeList();
            }


        }
    }
    public void OnListActiveChange(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        if (gameObject.activeSelf && !_actionWindow.activeSelf)
        {
            if (dir.y < 0 && _currentOrderItem < _itemsList.Count - 1 && !isNavigateButtonPressed)
            {
                _currentOrderItem++;
                ChangeList();
                isNavigateButtonPressed = true;
            }
            else if (dir.y > 0 && _currentOrderItem > 0 && !isNavigateButtonPressed)
            {
                _currentOrderItem--;
                ChangeList();
                isNavigateButtonPressed = true;
            }
            else if(dir.y == 0)
            {
                isNavigateButtonPressed = false;
            }
            
        }
        
    }

    public void OnActionWindowOpen(InputAction.CallbackContext context)
    {
        if(gameObject.activeSelf && context.performed && _currentOrderItem < _itemsList.Count)
        {
            _navigate.NumOfWindow = 2;
            _actionWindow.SetActive(true);
        }

    }
    public void DeleteItem()
    {
        if (_actionWindow.activeSelf)
        {
            MyDataBase.ExecuteQueryWithoutAnswer($"DELETE FROM Inventory WHERE id = {_itemsList[_currentOrderItem].ID}");
            UpdateList();
            ChangeList();
            _actionWindow.SetActive(false);
        }
    }
    public void UseItem()
    {
        if (_actionWindow.activeSelf)
        {
            GlobalEventsSystem.HeathUp();
            MyDataBase.ExecuteQueryWithoutAnswer($"DELETE FROM Inventory WHERE id = {_itemsList[_currentOrderItem].ID}");
            UpdateList();
            ChangeList();
            _actionWindow.SetActive(false);
        }
    }
    private void UpdateList()
    {
        _itemsList.Clear();
        SqliteDataReader reader = MyDataBase.GetReader($"SELECT * FROM Inventory WHERE type = {_currentSortType + 1} and player = {StaticInformation.id}");
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"].ToString());
                string name = reader["name"].ToString();
                int price = Convert.ToInt32(reader["price"].ToString());
                int quantity = Convert.ToInt32(reader["quantity"].ToString());
                int type = Convert.ToInt32(reader["quantity"].ToString());

                _itemsList.Add(new Item(id, name, price, quantity, type));
            }
        }
    }
    private void ChangeList()
    {

        int temp = _currentOrderItem - 3; 
        for(int i = 0; i < _visualItems.Length; i++)
        {
            try
            {
                _visualItems[i].SetActive(true);
                _visualItems[i].GetComponent<ItemField>().LoadItems(_itemsList[temp]);
            }
            catch
            {
                _visualItems[i].SetActive(false);
            }
            temp++;
        }
    }
    private void UpdateTitle()
    {
        _titleOfType.Clear();
        SqliteDataReader reader = MyDataBase.GetReader("SELECT * FROM TypeOfItems");

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                _titleOfType.Add(reader["type"].ToString());
            }
        }
        _title.text = _titleOfType[_currentSortType];
    }

}

public struct Item
{
    int _id;
    string _name;
    int _price;
    int _quantity;
    int _type;

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }
    public string NameOfItem
    {
        get { return _name; }
        set { _name = value; }
    }
    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }
    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
    public int Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public Item(int id, string name, int price, int quantity, int type)
    {
        _id = id;
        _name = name;
        _price = price;
        _quantity = quantity;
        _type = type;
    }
}

