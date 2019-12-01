using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTrackController : MonoBehaviour
{
    AudioSource music;
    private float musicVolume;
    public Slider musicSlider;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
        Init();
    }
    void Start()
    {
        musicSlider.value = musicVolume;
    }
    public void UpdateVolume()
    {
        music.volume = musicSlider.value;
        musicVolume = musicSlider.value;
    }

    private void Init()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = 1f;
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
            PlayerPrefs.Save();
        }
        else
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
            music.volume = musicVolume;
        }
    }

    void OnDestroy()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }
}
