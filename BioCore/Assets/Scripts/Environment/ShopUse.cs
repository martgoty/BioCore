 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class ShopUse : MonoBehaviour
{
    [SerializeField] private int _tovar;
    [SerializeField] private TextMeshProUGUI _money;

    int cost = 0;
    string nameTovar = "";
    int type = 0;
    private bool _canUse = false;

    private void Start() {
        cost = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select Price from Shop where ID = {_tovar}"));
        nameTovar = MyDataBase.ExecuteQueryWithAnswer($"select Name from Shop where ID = {_tovar}").ToString();
        type = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select Type from Shop where ID = {_tovar}"));
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            _canUse = true;


        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            _canUse = false;
        }
    }

    public void BuySomething(InputAction.CallbackContext context){
        if(context.performed){
            if(_canUse){
                if(Convert.ToInt32(_money.text) >= cost){
                    int count = Convert.ToInt32(MyDataBase.ExecuteQueryWithAnswer($"select count(quantity) from Inventory where name = \'{nameTovar}\'"));
                    if(count > 0){
                        count++;
                        MyDataBase.ExecuteQueryWithoutAnswer($"update Inventory set quantity = {count}");
                    }
                    else{
                        MyDataBase.ExecuteQueryWithoutAnswer($"insert into Inventory(name, price, quantity, type, player) values(\'{nameTovar}\', {cost}, 1, {type}, {StaticInformation.id})");
                    }
                    _money.text = (Convert.ToInt32(_money.text) - cost).ToString();

                }

            }

        }
    }
}
