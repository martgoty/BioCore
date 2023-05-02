using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Legacy
{
    public class UIController : MonoBehaviour
    {
        private Button startButton;
        private Button messageButton;
        private Button exitButton;
        private Button backButton;
        private Label warningText;
        private TextField field;
        private VisualElement mainMenu;
        private VisualElement scoreBoard;

        //-------------------------------------------------------------------
        [SerializeField] private VisualTreeAsset itemListTemplate;
        private List<ScoreBoard> scoreBoardList = new List<ScoreBoard>();
        ListView itemsListView;
        DataTable _players;
        DataTable _score;
        private int _increment = 0;//Delete this shit
        struct ScoreBoard
        {
            string _nickname;
            int _score;
            public string Nickname
            {
                get { return _nickname; }
                set { _nickname = value; }
            }

            public int Score
            {
                get { return _score; }
                set { _score = value; }
            }

            public ScoreBoard(string nickname, int score)
            {
                _nickname = nickname;
                _score = score;
            }
        }
        //-------------------------------------------------------------------
        void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            exitButton = root.Q<Button>("exit-button");
            startButton = root.Q<Button>("start-button");
            messageButton = root.Q<Button>("board-button");
            backButton = root.Q<Button>("back-button");
            warningText = root.Q<Label>("warning");
            field = root.Q<TextField>("field");
            mainMenu = root.Q<VisualElement>("main-menu");
            itemsListView = root.Q<ListView>("list");
            scoreBoard = root.Q<VisualElement>("score-board");
            startButton.clicked += StartButtonPressed;
            messageButton.clicked += MessageButtonPressed;
            backButton.clicked += BackButtonPressed;
            exitButton.clicked += Exit;
            //---------------------------------------------------------------
            DataConnection();
            _increment = _players.Rows.Count;
            //________________________________________________________________
        }

        void Exit()
        {
            Application.Quit();
        }

        void DataConnection()
        {
            _players = MyDataBase.GetTable("SELECT * FROM players");
            _score = MyDataBase.GetTable("SELECT * FROM score");
            for (int i = 0; i < _score.Rows.Count; i++)
            {
                scoreBoardList.Add(new ScoreBoard(_players.Rows[i][1].ToString(), int.Parse(_score.Rows[i][2].ToString())));
            }

            itemsListView.makeItem = () =>
            {
                return itemListTemplate.Instantiate();
            };

            itemsListView.bindItem = (_item, _index) =>
            {
                var item = scoreBoardList[_index];
                _item.Q<Label>("nickname").text = item.Nickname;
                _item.Q<Label>("score").text = item.Score.ToString();
            };

            itemsListView.itemsSource = scoreBoardList;


        }
        void BackButtonPressed()
        {
            mainMenu.style.display = DisplayStyle.Flex;
            scoreBoard.style.display = DisplayStyle.None;
        }
        void StartButtonPressed()
        {
            if (field.value != "")
            {
                MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO players VALUES ({_increment.ToString()}, \"{field.value}\")");
                _increment++;
                SceneManager.LoadScene(1);
            }
            else
            {
                warningText.text = "Need Name";
                warningText.style.display = DisplayStyle.Flex;
            }
        }
        void MessageButtonPressed()
        {
            mainMenu.style.display = DisplayStyle.None;
            scoreBoard.style.display = DisplayStyle.Flex;
        }


    }

}
