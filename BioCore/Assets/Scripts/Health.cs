using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private Image[] _hpImage;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private GameObject[] _uiElements;
    private int _hp;

    private void Awake()
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
        if (_uiElements[0].activeSelf || _uiElements[1].activeSelf)
        {
            _hpImage[0].enabled = false;
            _hpImage[1].enabled = false;
            _hpImage[2].enabled = false;
        }
        else
        {
            _hpImage[0].enabled = true;
            _hpImage[1].enabled = true;
            _hpImage[2].enabled = true;
        }
    }
    private void Damage()
    {
        _hp--;
        if(_hp > 0)
        {
            _hpImage[_hp].color = new Vector4(0f, 0f, 0f, 1f);
        }
        else
        {
            SceneManager.LoadScene(1);
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
}
