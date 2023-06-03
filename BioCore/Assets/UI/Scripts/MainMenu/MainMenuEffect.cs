using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuEffect : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private float _speedToScale;
    [SerializeField] private float _speedToHide;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == _button)
        {
            if ((_image.rectTransform.rect.width < _button.GetComponent<RectTransform>().rect.width))
            {
                _image.rectTransform.sizeDelta = new Vector2(_image.rectTransform.rect.width + _speedToScale * Time.deltaTime, _image.rectTransform.rect.height);
                _image.color = new Vector4(_image.color.r, _image.color.g, _image.color.b, Mathf.Lerp(0.4f, 0, _image.rectTransform.rect.width / _button.GetComponent<RectTransform>().rect.width));
            }
            else
            {
                _image.rectTransform.sizeDelta = new Vector2(0, _image.rectTransform.rect.height);
                _image.color = new Vector4(_image.color.r, _image.color.g, _image.color.b, 0.4f);
            }
        }
        else
        {
            _image.color = new Vector4(_image.color.r, _image.color.g, _image.color.b, 0f);
        }




    }

}
