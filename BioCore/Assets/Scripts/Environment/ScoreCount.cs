using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nick;
    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private int _level;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private GameObject _loding;
    [SerializeField] private GameObject _pressTxt;
    [SerializeField] private GameObject _loadingTxt;

    [SerializeField] private Slider _bar;
    private List<ScoreBoard> _scoreBoardList = new List<ScoreBoard>();
    private List<Logins> _loginsList = new List<Logins>();
    private float _timer = 0f;
    private float _totalScore = 1000f;
    private void Start() {
        
        GlobalEventsSystem.OnTakeCoin.AddListener(ScoreUp);
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            MyDataBase.ExecuteQueryWithoutAnswer($"update Logins set Level = {_level + 1}");
            _scoreBoard.SetActive(true);
            CountScore();
        }
    }

    private void Update(){
        _timer = Time.deltaTime * 1f;
    }

    private void ScoreUp(){
        _totalScore += 1000f;
    }
    public void CountScore(){
        _totalScore =_totalScore / (_timer * 1000);
        if(_totalScore <= 0){
            _totalScore = 78f;
        }
        int count = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select count(ID) from Score where ID = {StaticInformation.id} and Level = {SceneManager.GetActiveScene().buildIndex - 1}"));
        int currentscore = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select Score from Score where ID = {StaticInformation.id} and Level =  {SceneManager.GetActiveScene().buildIndex - 1}"));
        if(currentscore < Convert.ToInt32(_totalScore)){
            if(count > 0){
                MyDataBase.ExecuteQueryWithoutAnswer($"update Score set Score = {Convert.ToInt32(_totalScore)}");
            }
            else{
                MyDataBase.ExecuteQueryWithoutAnswer($"insert into Score(UserID, Level, Score) values({StaticInformation.id}, {SceneManager.GetActiveScene().buildIndex - 1}, {Convert.ToInt32(_totalScore)})");
            }

        }
        SqliteDataReader reader = MyDataBase.GetReader($"select * from Score where Level = {SceneManager.GetActiveScene().buildIndex - 1}");
        while(reader.Read()){
            int id = Convert.ToInt32(reader["UserID"]);
            int score = Convert.ToInt32(reader["Score"]);
            _scoreBoardList.Add(new ScoreBoard(id, score));
        }

        reader = MyDataBase.GetReader($"select * from Logins");
        while(reader.Read()){
            int id = Convert.ToInt32(reader["ID"]);
            string name = reader["Login"].ToString();
            _loginsList.Add(new Logins(id, name));
        }

        _nick.text = "";
        _score.text = "";
        foreach(var score in _scoreBoardList)
        {
            _score.text += $"{score.Score} \n";
            _nick.text += $"{_loginsList.Find(item => item.Id == score.Id).Name}";
        }

        Time.timeScale = 0f;
    }

    public void NextLevel(int level){
        _scoreBoard.SetActive(false);
        _loding.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(Loading(level));
    }

    IEnumerator Loading(int level){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone){
            _bar.value = asyncLoad.progress;
            if(asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation){
                MyDataBase.ExecuteQueryWithoutAnswer("select * from Inventory");
                _pressTxt.SetActive(true);
                _loadingTxt.SetActive(false);
                _bar.value = 1f;
                if(Input.anyKeyDown){
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}

struct ScoreBoard
{
    int _id;
    int _score;

    public int Id
    {
        get{return _id;}
        set{_id = value;}
    }
    public int Score
    {
        get{return _score;}
        set{_score = value;}
    }

    public ScoreBoard(int id, int score){
        _id = id;
        _score = score;
    }
}

struct Logins
{
    int _id;
    string _name;

    public int Id
    {
        get{return _id;}
        set{_id = value;}
    }
    public string Name
    {
        get{return _name;}
        set{_name = value;}
    }

    public Logins(int id, string name)
    {
        _id = id;
        _name = name;
    }
}

