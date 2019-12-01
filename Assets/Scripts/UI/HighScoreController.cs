using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    public DataBankController dataBank;
    public Text highScoreNames;
    public Text highScoreScores;
    public Text yourScores;
    void Start()
    {
        UpdateHighScores();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateNames()
    {
        highScoreNames.text = dataBank.GetHighScoresNames();
    }

    private void UpdateScores()
    {
        highScoreScores.text = dataBank.GetHighScoresScores();
    }

    private void UpdatePlayerScore() {
        yourScores.text = "Your score: " + dataBank.GetPlayersHighScore(); 
    }

public void UpdateHighScores()
{
    UpdatePlayerScore();
    UpdateNames();
    UpdateScores();
}
}
