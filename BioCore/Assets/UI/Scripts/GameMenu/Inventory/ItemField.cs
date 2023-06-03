using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemField : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _name;
    [SerializeField]private TextMeshProUGUI _quantity;
    [SerializeField]private Image _icon;
    public void LoadItems(Item item)
    {
        _name.text = item.NameOfItem.ToString();
        _quantity.text = item.Quantity.ToString();
        _icon.sprite = Resources.Load<Sprite>(_name.text);
    }

}
