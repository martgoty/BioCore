using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class LevelsSetActive : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private GameObject _loading;
    [SerializeField] private Slider _bar;

    [SerializeField] private GameObject _pressTxt;
    [SerializeField] private GameObject _loadingTxt;


    private void Start() {
        int count = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select Level from Logins where ID = {StaticInformation.id}"));
        for(int i = 0; i <= count; i++){
            _buttons[i].GetComponent<Button>().interactable = true;
        }
    }

    // public void Level0(int level){
    //     _loading.SetActive(true);
    //     StartCoroutine(Loading(level + 1));
    // }

    // IEnumerator Loading(int level){
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
    //     asyncLoad.allowSceneActivation = false;
    //     while (!asyncLoad.isDone){
    //         _bar.value = asyncLoad.progress;
    //         if(asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation){
    //             MyDataBase.ExecuteQueryWithoutAnswer("select * from Inventory");
    //             _pressTxt.SetActive(true);
    //             _loadingTxt.SetActive(false);
    //             _bar.value = 1f;
    //             if(Input.anyKeyDown){
    //                 yield return null;
    //                 asyncLoad.allowSceneActivation = true;
    //             }
    //         }

    //         yield return null;
    //     }
    // }
}
