using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeActiveSortButton : MonoBehaviour
{

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _unactiveColor;

    public void SetActive()
    {
        GetComponent<Image>().color = _activeColor;
    }

    public void DropActive()
    {
        GetComponent<Image>().color = _unactiveColor;
    }
}
