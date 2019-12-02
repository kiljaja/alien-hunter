using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider fxVolume;
    public Toggle hardMode;
    public InputField playerName;
    
    void Start()
    {
        playerName.text = PlayerPrefs.GetString("playerName");
        fxVolume.value = PlayerPrefs.GetFloat("fxVolume");
        hardMode.isOn = ToBool(PlayerPrefs.GetInt("isHardMode"));
    }
    private bool ToBool(int num)
    {
        return num == 1;
    }

    public void UpdatePlayerName()
    {
        string name = playerName.text;
        PlayerPrefs.SetString("playerName", name);
    }

    public void UpdateFXVolume()
    {
        PlayerPrefs.SetFloat("fxVolume", fxVolume.value);
    }

    public void UpdateHardMode()
    {
        int num = (hardMode.isOn) ? 1 : 0;
        PlayerPrefs.SetInt("isHardMode", num);
    }
}
