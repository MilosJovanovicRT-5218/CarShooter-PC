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
        SceneManager.LoadScene("Level1");//Pritiskom n Play Button pokrece se scena Level1
    }

    public void OpenSettings()
    {
        settingsImage.SetActive(true);//klikom na settings otvori setting ,odnosno stavi ga na true da bude vidljiv
    }

    public void CloseSettings()
    {
        settingsImage.SetActive(false);//klikom na settings zatvori setting ,odnosno stavi ga na false da bude nevidljiv
    }

    //Full screen
    private bool isFullScreen = false;//bool provera da li je vec FullScreen ili ne 

    public void ChangeScreenState()//Metoda za promenu stanja ekrana ,odnosno full screne ili window,sve to na toggle element(CheckBox)
    {
        isFullScreen = !isFullScreen;//sluzi kao neka vrsta invertora ,znaci ako je neko stanje bilo true bice false ,ako je bila false bice true

        if (isFullScreen)//Na osnovu komentara iznacn znaci kad se klikne na toggle bice false 
        {
            int screenWidth = 800; // širina prozora u pikselima
            int screenHeight = 600; // visina prozora u pikselima
            Screen.SetResolution(screenWidth, screenHeight, false);
        }
        else//ovo je samo suprotno od if uslova znaci bice true
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
    }

    //Sound
    public Slider volumeSlider; // Referenca na slider za glasnoću
    public AudioSource[] musicSources; // Polje svih audio izvora za glazbu na sceni/Odnosno lista u kojoj sam dodao sve audio sourc koje zelim da kontrolisem preko slider

    private void Start()
    {
        Time.timeScale = 1.0f;//pokreni vreme

        // Postavljanje početne vrijednosti slidera na trenutnu glasnoću
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);//Player Prefs sluzi za cuvanje podataka nakon gasenja igre ili promene scene u ovom slucaju koristi se za cuvanje slider value ,ovo 0.5f je bilo na pocetku da mu postavi pocetnu vrednost slider ,moze da stoji bilo sta od 0 do 1  i moze da bude i int vrednost(samo pre zagrade mora da sroji GetInt umesto GetFloat) ne mora kao sad float

        // Dodavanje slušatelja događaja za promjenu vrijednosti slidera
        volumeSlider.onValueChanged.AddListener(ChangeMusicVolume);//dodaje listener na slider da ne bih morao da dodajem ovu metodu na slider na on clik ovako samo prevuce slider u public Slider volumeSlider;

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
        Application.Quit();//Ovo sam povezao sa btn na OnClik ,dodao sam skriptu i poveza Metodu OnAplicationQuit,zato je public metoda da je private ne bih mogao da je dodam i ne bi bila javno dostupna za OnClik ili za radi iz druge skripte pomocu instance odnosno Singleton patterna ,koji za to i sluzi da moze da vrsis neke promene iz druge skripte itd.
    }

}
