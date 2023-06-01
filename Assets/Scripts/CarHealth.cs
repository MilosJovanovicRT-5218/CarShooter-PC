using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    //public GameObject deadBoss;
    
    public Slider healthSlider; //Referenca na Slider UI element

    public GameObject dieImage;//Slika koja je kativna kada je igrac mrtav

    public GameObject gun1, gun2;//Gun objekti

    public AudioSource pickHealthSound;//Zvuk za pick health

    private void Start()
    {
        Time.timeScale = 1.0f;//pokreni vreme ,ovo sam dodao jer kada se klikne restart vreme je 0 odnsosno pauzirano je tako da kad klikne restart pokrene ponovo scenu i vreme krece ponovo ,nije pauzirano 

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        dieImage.SetActive(false);//die Image je false jer nema razloga da na startu bude true jer je igrac ziv i ima maksimalan Health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;//sluzi sa smanjivanje health 

        if (currentHealth <= 0)//Ako je health 0 ili manje onda uradi to sto je unutar uslova
        {
            Time.timeScale = 0f;//zaustavi vreme da ne bi Player mogao da se krece a i Ai/Enemy
            Die();
        }

        UpdateHealthBar();
    }

    public void Die()//MEtoda koja se aktivira kada je Player mrtav 
    {
        // implementirajte ono što treba da se desi kada igrač umre (npr. eksplodira, nestane, itd.)
        dieImage.SetActive(true);//Posto je umro aktiviraj dieImage
        gun1.SetActive(false);//disablegun da ne bi mogao da puca
        gun2.SetActive(false);//disablegun da ne bi mogao da puca
    }

    private void Update()
    {
        healthSlider.value = currentHealth;//Proveravaj non stop vrednost health,ako ima uslov da pokupi health azuzriraj vrednost i prikazi da je dodao ,to radi svake sekunde 
        //Fixed Update provera vrednosti na svakih 0.4 sekunde Update svake sekunde 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wave1")//Ako colide sa objektom sa tim tagom
        {
            TakeDamage(5); // skidanje 20 zdravlja nakon udarca metka
        }
        else if (other.gameObject.tag == "Wave2")//Ako colide sa objektom sa tim tagom
        {
            TakeDamage(5); // skidanje 20 zdravlja nakon udarca metka
        }
        else if (other.gameObject.tag == "WaveBoss")//Ako colide sa objektom sa tim tagom
        {
            TakeDamage(5); // skidanje 20 zdravlja nakon udarca metka
        }

        if (other.tag == "Health" && currentHealth <= 95)//Moze da pokupui health alp colide sa Game Object-om sa tag-om Health i ako ima manuje od 95 health-a
        {
            currentHealth += 5;//dodaj 5 health-a ako je ispunjen uslov
            Destroy(other.gameObject);//Takodje unitsi unisti Game Object
            pickHealthSound.Play();//Aktiviraj sound za pick helath
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ResetLevel1()
    {
        SceneManager.LoadScene("Level1");//Otvori scenu Level1
    }

    public void ResetLevel2()
    {
        SceneManager.LoadScene("Level2");//Otvori scenu Level2
    }

    public void ResetLevel3()
    {
        SceneManager.LoadScene("Level3");//Otvori scenu Level3
    }
}