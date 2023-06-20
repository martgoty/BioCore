using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private int _price;

    [SerializeField] private float _offset;
    [SerializeField] private float _speed;
    private float _yPos;
    bool _isUp = true;

    private void Start() {
        _yPos = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            gameObject.GetComponentInParent<AudioSource>().Play();
            int money = Convert.ToInt32(_money.text) + _price;
            _money.text = money.ToString();
            GlobalEventsSystem.TakeCoin();
        }
        Destroy(gameObject);
    }

    private void Update() {
        if(_isUp){
            if(transform.position.y <= _yPos +_offset){
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }else{
                _isUp = false;
            }

        }
        else{
            if(transform.position.y >= _yPos -_offset){
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }else{
                _isUp = true;
            }
        }
    }
}
