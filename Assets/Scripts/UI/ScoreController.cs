using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private TextMeshPro textContainer;
    void Start()
    {
        if (textContainer == null) textContainer = GetComponentInChildren<TextMeshPro>();
        UpdateScore(GetScore());

    }

    public void UpdateScore(int score){
        if(score < 0) score = 0;
        string formatedScore = score.ToString().PadLeft(3, '0');
        textContainer.text = formatedScore;
    }

    public int GetScore(){
        return 200;
    }
}
