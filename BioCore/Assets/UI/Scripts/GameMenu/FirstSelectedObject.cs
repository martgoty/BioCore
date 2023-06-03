using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelectedObject : MonoBehaviour
{
    [SerializeField] private GameObject _firstSelectedObj;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelectedObj);
    }
}
