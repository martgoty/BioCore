using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisen : MonoBehaviour
{
    [SerializeField] GameObject _sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Inventory (name, price,quantity,type, player) VALUES ('Зелье здоровья', 100, 1, 2, {StaticInformation.id})");
            _sound.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
