using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Authorization : MonoBehaviour
{

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _authorization;
    [SerializeField] private GameObject _register;
    [SerializeField] private TextMeshProUGUI _login;
    [SerializeField] private TextMeshProUGUI _password;
    [SerializeField] private TextMeshProUGUI _error;
    [SerializeField] private TextMeshProUGUI _error1;
    [SerializeField] private TextMeshProUGUI _login1;
    [SerializeField] private TextMeshProUGUI _password1;
    [SerializeField] private TextMeshProUGUI _password2;

    public void OnEnterPressed()
    {
         
        if(_login.text != null && _password.text != null)
        {
            int count = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select count(ID) from Logins where Login = \'{_login.text}\' and Password = \'{_password.text}\'"));
            if(count > 0){
                gameObject.SetActive(false);
                _menu.SetActive(true);
            }
            else{
                _error.text = "Неверный логин или пароль";
            }
        }
        else{
            _error.text = "Неверный логин или пароль";
        }
    }

    public void OnBackPressed(){
        if(_register.activeSelf){
            _register.SetActive(false);
            _authorization.SetActive(true);
            _error.text = "";
            _error1.text = "";
        }
    }

    public void OpenRegister(){
        _register.SetActive(true);
        _authorization.SetActive(false);
        _error.text = "";
        _error1.text = "";
    }

    public void Register(){
        if(_login1.text != null && _password1.text != null && _password2.text != null){
            if(_password1.text == _password2.text){
                if(Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select count(ID) from Logins where Login = \'{_login1.text}\'")) == 0){
                    MyDataBase.ExecuteQueryWithoutAnswer($"insert into Logins(Login, Password, Level) values (\'{_login1.text}\', \'{_password1.text}\', 0)");
                    StaticInformation.login = _login1.text;
                    StaticInformation.id = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select ID from Logins where Login = \'{_login1.text}\'"));
                    OnBackPressed();
                }
                else{
                    _error1.text = "Такой пользователь уже";
                }
            }
            else{
                _error1.text = "Пароли не совпадают";
            }
        }
        else{
            _error1.text = "Не все поля заполнены";
        }
    }

}
