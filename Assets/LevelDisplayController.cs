using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelDisplayController : MonoBehaviour
{
    private TextMeshPro textContainer;
    void Start()
    {
        if (textContainer == null) textContainer = GetComponentInChildren<TextMeshPro>();
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        textContainer.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }
}
