using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShoot : MonoBehaviour
{
    public GameObject bulletPrefab;  // prefab za metak
    public Transform gunBarrel;      // transform komponenta za mesto ispaljivanja metka
    public float bulletSpeed = 10f;  // brzina metka
    public int damage = 33;           // koliko štete metak nanosi
    public AudioSource shootSound;
    //public AudioSource audioShoot;//Shoot sound

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);  // napravi novi metak iz prefab-a
            Rigidbody rb = bullet.GetComponent<Rigidbody>();  // uzmi rigidbody komponentu metka
            rb.velocity = gunBarrel.forward * bulletSpeed;  // postavi brzinu metka u pravcu u kojem je igrač uperio oružje
            Destroy(bullet, 2f);  // uništi metak nakon 2 sekunde (da ne bi ostao u sceni zauvek)

            // emituj zvuk pucnja, ako postoji AudioSource na ovom objektu
           shootSound.Play();
        }
    }
}
