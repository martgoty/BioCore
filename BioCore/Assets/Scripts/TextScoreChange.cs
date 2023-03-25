using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextScoreChange : MonoBehaviour
{
    public int _score = 0;
    [SerializeField] private Text _textMeshPro;
    private void Awake()
    {
        GlobalEventsSystem.OnPlayerTakeCoin.AddListener(ScoreUp);
    }

    void ScoreUp()
    {
        _score++;
        _textMeshPro.text = "Score: " + _score.ToString();
    }
}
