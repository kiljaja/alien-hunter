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
    }
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    public void UpdateVolume()
    {
        music.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
