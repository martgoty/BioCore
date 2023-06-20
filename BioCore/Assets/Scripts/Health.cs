using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private Image[] _hpImage;
    [SerializeField] private Image _moneyImage;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private GameObject[] _uiElements;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private Slider _bar;
    [SerializeField] private GameObject _loadingTxt;
    [SerializeField] private GameObject _pressTxt;
    [SerializeField] private GameObject _loading;
    private int _hp;

    private void Start()
    {
        GlobalEventsSystem.OnTakeDamage.AddListener(Damage);
        GlobalEventsSystem.OnHealth.AddListener(HealthUp);
        _hp = _hpImage.Length;
        
        string money;
        try{
            money = MyDataBase.ExecuteQueryWithAnswer($"select Money from Logins where Login = \'{StaticInformation.login}\'").ToString();
        }
        catch{
            money = "0";
        }
        _money.text = money;

    }
    private void Update()
    {
        if (_uiElements[0].activeSelf || _uiElements[1].activeSelf || _uiElements[2].activeSelf)
        {
            _hpImage[0].enabled = false;
            _hpImage[1].enabled = false;
            _hpImage[2].enabled = false;
            _moneyImage.enabled = false;
            _money.enabled =false;
        }
        else
        {
            _hpImage[0].enabled = true;
            _hpImage[1].enabled = true;
            _hpImage[2].enabled = true;
            _moneyImage.enabled = true;
            _money.enabled = true;
        }
    }
    private void Damage()
    {
        _hp--;
        if(_hp > 0)
        {
            _hpImage[_hp].color = new Vector4(0f, 0f, 0f, 1f);
            _sound.Play();
        }
        else
        {
            _loading.SetActive(true);
            StartCoroutine(Loading(SceneManager.GetActiveScene().buildIndex));
        }
    }

    private void HealthUp()
    {
        if(_hp < _hpImage.Length)
        {
            _hpImage[_hp].color = new Vector4(1f, 1f, 1f, 1f);
            _hp++;
            Debug.Log(_hp);
        }
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
