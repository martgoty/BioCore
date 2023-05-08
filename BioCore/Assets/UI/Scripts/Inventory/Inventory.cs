using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemField _itemTemplate;
    [SerializeField] private Transform _container;  

    private void OnEnable()
    {
        SqliteDataReader reader = MyDataBase.GetReader("SELECT * FROM Inventory WHERE type = 1" );

        if (reader.HasRows)
        {
            TemplateInstantiate(reader);
        }
    }

    private void TemplateInstantiate(SqliteDataReader reader)
    {
        while (reader.Read())
        {
            var cell = Instantiate(_itemTemplate, _container);
            cell.LoadItems(reader);
        }
    }

    public void UpdateItems(string type)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        SqliteDataReader reader = MyDataBase.GetReader($"SELECT * FROM Inventory WHERE type = {type}");
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                var cell = Instantiate(_itemTemplate, _container);
                cell.LoadItems(reader);
            }
        }
    }

}

