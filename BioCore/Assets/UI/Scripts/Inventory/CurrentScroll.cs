using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CurrentScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _sv;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sv.verticalNormalizedPosition = _sv.verticalNormalizedPosition - 50f;
        }
    }
}
