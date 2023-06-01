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
    
    public Slider healthSlider; // referenca na Slider UI element

    public GameObject dieImage;

    public GameObject gun1, gun2;

    public AudioSource pickHealthSound;

    private void Start()
    {
        Time.timeScale = 1.0f;

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        dieImage.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            Die();
        }

        UpdateHealthBar();
    }

    public void Die()
    {
        // implementirajte ono što treba da se desi kada igrač umre (npr. eksplodira, nestane, itd.)
        dieImage.SetActive(true);
        gun1.SetActive(false);
        gun2.SetActive(false);
    }

    private void Update()
    {
        healthSlider.value = currentHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wave1")
        {
            TakeDamage(5); // skidanje 20 zdravlja nakon udarca metka
        }
        else if (other.gameObject.tag == "Wave2")
        {
            TakeDamage(5); // skidanje 20 zdravlja nakon udarca metka
        }
        else if (other.gameObject.tag == "WaveBoss")
        {
            TakeDamage(5); // skidanje 20 zdravlja nakon udarca metka
        }

        if (other.tag == "Health" && currentHealth <= 95)
        {
            currentHealth += 5;
            Destroy(other.gameObject);
            pickHealthSound.Play();
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
        SceneManager.LoadScene("Level1");
    }

    public void ResetLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void ResetLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
}