using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossState : MonoBehaviour
{
    public GameObject boss1; // Referenca na prvi objekt
    public GameObject boss2; // Referenca na drugi objekt

    private bool object1Destroyed = false;
    private bool object2Destroyed = false;

    public GameObject winImage;

    void Update()
    {
        // Provjeri je li prvi objekt uništen
        if (!object1Destroyed && boss1 == null)
        {
            Debug.Log("Prvi boss je unisten.");
            object1Destroyed = true;
        }

        // Provjeri je li drugi objekt uništen
        if (!object2Destroyed && boss2 == null)
        {
            Debug.Log("Drugi boss je unisten.");
            object2Destroyed = true;
        }

        // Provjeri jesu li oba objekta nestala
        if (object1Destroyed && object2Destroyed)
        {
            Debug.Log("Oba Boss-a su.");
            winImage.SetActive(true);
            // Možete ovdje izvršiti dodatne radnje koje želite nakon što oba objekta nestanu
        }
    }
}
