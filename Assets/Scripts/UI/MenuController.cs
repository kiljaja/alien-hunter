using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttonGroup;
    public GameObject howToPlayInfo;
    public GameObject settings;
    public GameObject highScores;

    public GameObject closeButton;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            StartMenu();
        }
    }

    public void StartMenu()
    {
        buttonGroup.SetActive(true);
        howToPlayInfo.SetActive(false);
        settings.SetActive(false);
        highScores.SetActive(false);
        closeButton.SetActive(false);
    }

    public void GoToHowToPlay()
    {
        GoToAnOption();
        howToPlayInfo.SetActive(true);
    }

    public void GoToSettings()
    {
        GoToAnOption();
        settings.SetActive(true);
    }

    public void GoToHighScores()
    {
        GoToAnOption();
        highScores.SetActive(true);
    }

    public void GoToAnOption()
    {
        buttonGroup.SetActive(false);
        closeButton.SetActive(true);
    }

    public void PlayButtonHandler()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "IntroScene")
        {
            SceneManager.LoadScene("Level1");
        }
        else if (scene.name == "EndScene")
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            ToggleMenu();
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();

        #if (UNITY_EDITOR) //Handle using the editor
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE) //Stand alone
            Application.Quit();
        #elif (UNITY_WEBGL) // WebGl 
            Application.OpenURL("about:blank");
        #endif //all other cases
            Application.Quit();
    }
}
