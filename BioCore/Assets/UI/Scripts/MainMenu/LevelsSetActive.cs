using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class LevelsSetActive : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;

    int count = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select Level from Logins where Login = \'{StaticInformation.login}\'"));

    private void Start() {
        for(int i = 0; i <= count; i++){
            _buttons[i].SetActive(true);
        }
    }

    public void Level0(){
        SceneManager.LoadScene(1);
    }
}
