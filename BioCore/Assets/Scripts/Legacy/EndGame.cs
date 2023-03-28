using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TextScoreChange _score;
    [SerializeField] private Timer _time;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int totalPoint = Convert.ToInt32(_score._score * (1/_time._timeLeft) * 100);
            DataTable scoreTable = MyDataBase.GetTable("SELECT * FROM score");

            int _index = scoreTable.Rows.Count;
            MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO score VALUES ({_index.ToString()},{_index.ToString()},{totalPoint.ToString()})");
            SceneManager.LoadScene("mainMenu");
        }
    }
}
