using UnityEngine;
using UnityEngine.UI;

public class ChangeActiveSortButton : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private string _numOfType;
    [SerializeField] private Inventory _inventory;

    public void ChangeActive()
    {
        GetComponent<Button>().interactable = false;
        foreach(var button in _buttons)
        {
            button.interactable = true;
        }

        _inventory.UpdateItems(_numOfType);         
    }
}
