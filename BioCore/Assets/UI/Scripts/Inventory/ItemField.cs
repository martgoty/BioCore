using Mono.Data.Sqlite;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemField : MonoBehaviour
{
    private int _id;
    [SerializeField]private TextMeshProUGUI _name;
    [SerializeField]private TextMeshProUGUI _price;
    [SerializeField]private TextMeshProUGUI _quantity;
    [SerializeField]private Image _icon;
    public void LoadItems(SqliteDataReader reader)
    {
        _name.text = reader["name"].ToString();
        _price.text = reader["price"].ToString();
        _quantity.text = reader["quantity"].ToString();
        _icon.sprite = Resources.Load<Sprite>(_name.text);
        _id = Convert.ToInt32(reader["id"].ToString());

    }

}
