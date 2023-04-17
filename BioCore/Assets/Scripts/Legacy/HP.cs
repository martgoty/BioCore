using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private Image[] _sprite;

    private void Start()
    {
        GlobalEventsSystem.OnPlayerTakeDamage.AddListener(ReduceHealthPoint);
        GlobalEventsSystem.OnPlayerTakeHealth.AddListener(UpHealthPoint);
    }
    public void ReduceHealthPoint()
    {
        if(_hp > 1)
        {
            _hp--;
            _sprite[_hp].color = Color.black;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void UpHealthPoint()
    {
        if (_hp < 3)
        {
            _sprite[_hp].color = Color.white;
            _hp++;

        }
    }

}
