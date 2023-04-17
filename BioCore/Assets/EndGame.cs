using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] Timer timer;
    DataTable _players;
    DataTable _score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int temp = Convert.ToInt32(score.score * (1.0/timer._timeLeft * 100));
            _players = MyDataBase.GetTable("SELECT * FROM players");
            _score = MyDataBase.GetTable("SELECT * FROM score");
            MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO score VALUES ({_score.Rows.Count + 1} , {_players.Rows.Count - 1},{temp})");
            SceneManager.LoadScene(0);
        }
    }
}
