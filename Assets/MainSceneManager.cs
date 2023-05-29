using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public GameObject settingsImage;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenSettings()
    {
        settingsImage.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsImage.SetActive(false);
    }

    //Full screen
    private bool isFullScreen = false;

    public void ChangeScreenState()
    {
        isFullScreen = !isFullScreen;

        if (isFullScreen)
        {
            int screenWidth = 800; // širina prozora u pikselima
            int screenHeight = 600; // visina prozora u pikselima
            Screen.SetResolution(screenWidth, screenHeight, false);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
    }

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

    //Exit Game

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}
