using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
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

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    public void Die()
    {
        // implementirajte ono što treba da se desi kada igrač umre (npr. eksplodira, nestane, itd.)
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(10); // skidanje 20 zdravlja nakon udarca metka
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }
}
