using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;//Maksimalni health koji Ai/Enemy moze da ima 
    public int currentHealth;//Trenutni health
    //public GameObject deadBoss;

    public Slider healthSlider; // referenca na Slider UI element

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)//Ako ima 0 ili manje health aktiviraj Metodu Die();
        {
            Die();
        }

        UpdateHealthBar();
    }

    public void Die()
    {
        // implementirajte ono što treba da se desi kada igrač umre (npr. eksplodira, nestane, itd. u ovom slucaju unisti Game Object)
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")//Ako ako colide sa objektom koji ima tag Bullet onda...
        {
            TakeDamage(10); // skidanje 20 zdravlja nakon udarca metka
        }

        if (other.gameObject.tag == "Player")//Ako ako colide sa objektom koji ima tag Player onda...
        {
            TakeDamage(20); // ako plauer udari Ai/Enemy skine mu -50 na health
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }
}
