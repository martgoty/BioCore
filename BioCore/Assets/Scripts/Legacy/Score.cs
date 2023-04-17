using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text _txt;
    public int score = 0;

    private void Start()
    {
        GlobalEventsSystem.ScoreUp.AddListener(Update123);
        _txt.text = "Score: " + score.ToString();
    }

    private void Update123()
    {
        score++;
        _txt.text = "Score: " + score.ToString();
    }

}
