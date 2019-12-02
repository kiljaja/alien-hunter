using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public MenuController menu;
    public ScoreController scorePanel;

    private GameObject enemy;

    private int playerScore;

    void Start()
    {
        playerScore = PlayerPrefs.GetInt("playerScore");
        enemy = GameObject.FindWithTag("Enemy");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            PauseResume();
        }

        if(shouldLoadNextScene()){
            LoadNextScene();
        }
    }

    public void PauseResume(){
        if(Time.timeScale == 1){
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
        menu.ToggleMenu();
    }

    public void IncreaseScore(int value){
        if(value < 0) return;
        playerScore += value;
        PlayerPrefs.SetInt("playerScore", playerScore);
        scorePanel.UpdateScore(playerScore);
    }

    public void DecreaseScore(int value){
        if(value < 0) return;
        playerScore -= value;
        if(playerScore < 0) playerScore = 0;
        PlayerPrefs.SetInt("playerScore", playerScore);
        scorePanel.UpdateScore(playerScore);
    }

    public void LoadNextScene(){
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private bool shouldLoadNextScene(){
        return enemy == null;
    }




}


