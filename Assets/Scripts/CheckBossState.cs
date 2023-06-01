using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossState : MonoBehaviour
{
    public GameObject boss1; // Referenca na prvi objekt
    public GameObject boss2; // Referenca na drugi objekt

    private bool object1Destroyed = false;//Bool vrednost koja ce da bude trrue kada nestane prvi Boss1
    private bool object2Destroyed = false;//Bool vrednost koja ce da bude trrue kada nestane prvi Boss2

    public GameObject winImage;//Slika ako su bijieni oba Boss-a

    void Update()
    {
        // Provjeri je li prvi objekt uništen
        if (!object1Destroyed && boss1 == null)//Ako je Boss1 null ako ne postoji onda 
        {
            Debug.Log("Prvi boss je unisten.");
            object1Destroyed = true;
        }

        // Provjeri je li drugi objekt uništen
        if (!object2Destroyed && boss2 == null)//Ako je Boss2 null ako ne postoji onda 
        {
            Debug.Log("Drugi boss je unisten.");
            object2Destroyed = true;
        }

        // Provjeri jesu li oba objekta nestala
        if (object1Destroyed && object2Destroyed)//Proverava da li su oba Boss-a nestala,ako jesu aktiviraj Win Image
        {
            Debug.Log("Oba Boss-a su.");//Debug log koji samo ispisuje poruku radi provere nema neku specijalnu svrhu u kod sem debug provere itd.
            Time.timeScale = 0f;//Podesio sam vreme na 0 da ne bi igrac mogao da se krece dok je Win Image aktivan
            winImage.SetActive(true);//Samo stavlja da je objekat aktivan, odnsosno  vidlujiv na sceni 

            //Ovo za Win Image je moglo i ovako 
            //public Image winImage;
            //pa ovde samo winImage.enabled = true;//rezultat bi bio isti 
        }
    }
}
