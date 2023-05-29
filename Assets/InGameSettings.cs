using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettings : MonoBehaviour
{
    //Sound
    public Slider volumeSlider; // Referenca na slider za glasnoću
    public AudioSource[] musicSources; // Polje svih audio izvora za glazbu na sceni

    private void Start()
    {
        // Postavljanje početne vrijednosti slidera na trenutnu glasnoću
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        // Dodavanje slušatelja događaja za promjenu vrijednosti slidera
        volumeSlider.onValueChanged.AddListener(ChangeMusicVolume);

        // Postavljanje početne glasnoće za sve audio izvore glazbe
        SetMusicVolume(volumeSlider.value);
    }

    private void ChangeMusicVolume(float volume)
    {
        SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    private void SetMusicVolume(float volume)
    {
        foreach (AudioSource musicSource in musicSources)
        {
            musicSource.volume = volume;
        }
    }
}
