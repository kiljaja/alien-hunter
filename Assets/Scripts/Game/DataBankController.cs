using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataBankController : MonoBehaviour
{
    private const string NAME_KEY = "HSName";
    private const string SCORE_KEY = "HSScore";
    private const int NUM_HIGH_SCORES = 5;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("isInitialized"))
        {
            InitData();
        }
        else if (IsNewGame())
        {
            NewGameData();
        }
        else if (IsResetGame())
        {
            ResetHealthScore();
        }
        else if (IsEndOfGame())
        {
            SaveScores();
        }
    }

    private void NewGameData()
    {
        PlayerPrefs.SetString("playerName", "player");
    }

    private bool IsNewGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name == "IntroScene";
    }

    private bool IsResetGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name == "Level1";
    }

    private void ResetHealthScore()
    {
        PlayerPrefs.SetInt("playerHealth", 200);
        PlayerPrefs.SetInt("playerScore", 0);
    }

    private bool IsEndOfGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name == "EndScene";
    }

    private void InitData()
    {
        PlayerPrefs.SetInt("isInitialized", 1);
        PlayerPrefs.SetFloat("musicVolume", 1f);
        PlayerPrefs.SetFloat("fxVolume", 1f);
        PlayerPrefs.SetInt("isHardMode", 0);
        MockHighScore();
    }

    private void MockHighScore()
    {
        PlayerPrefs.SetString("HSName0", "Olga");
        PlayerPrefs.SetInt("HSScore0", 100);
        PlayerPrefs.SetString("HSName1", "felipe");
        PlayerPrefs.SetInt("HSScore1", 90);
        PlayerPrefs.SetString("HSName2", "Connie");
        PlayerPrefs.SetInt("HSScore2", 80);
        PlayerPrefs.SetString("HSName3", "Luigi");
        PlayerPrefs.SetInt("HSScore3", 70);
        PlayerPrefs.SetString("HSName4", "Emilio");
        PlayerPrefs.SetInt("HSScore4", 20);

    }

    // Based on Professor Dr. Devorah Kletenik Solution		
    private void SaveScores()
    {
        string pName = PlayerPrefs.GetString("playerName");
        pName = (pName.Length == 0)? "player" : pName;
        int pScore = PlayerPrefs.GetInt("playerScore");

        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            string curNameKey = NAME_KEY + i;
            string curScoreKey = SCORE_KEY + i;

            if (!PlayerPrefs.HasKey(curScoreKey)) //no ith score stored yet
            {
                PlayerPrefs.SetInt(curScoreKey, pScore);
                PlayerPrefs.SetString(curNameKey, pName);
                return;
            }

            else //there is an ith score
            {
                int score = PlayerPrefs.GetInt(curScoreKey); //score is currently ith highest

                if (pScore >= score) //this score should replace previous ith highest -- note deliberations over equality
                {
                    int tempScore = score;
                    string tempName = PlayerPrefs.GetString(curNameKey);

                    PlayerPrefs.SetInt(curScoreKey, pScore);
                    PlayerPrefs.SetString(curNameKey, pName);

                    pName = tempName;
                    pScore = tempScore;

                }

            }
        }
    }

    public string GetHighScoresScores()
    {
        string output = "";
        int numPadding = 3;
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            output += PlayerPrefs.GetString(NAME_KEY + i) + "   " + PlayerPrefs.GetInt(SCORE_KEY + i).ToString().PadLeft(numPadding, '0') + "\n";
        }
        return output;
    }

    public string GetPlayersHighScore()
    {
        return PlayerPrefs.GetString("playerName") + "   " + PlayerPrefs.GetInt("playerScore");
    }

}
