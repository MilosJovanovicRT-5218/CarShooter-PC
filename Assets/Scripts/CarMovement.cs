using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f;//Brzina kretanja Player-a
    public float rotationSpeed = 200f;//Brzina rotacije Player-a
    public float shiftSpeedMultiplier = 3;//Dodaj +3 na brzinu ako se kativira nitro 

    private Rigidbody rb;//Rig Body 

    public GameObject inGameSettings;//Slika Settings

    public GameObject nitro;//Nitro ovo je samo vizualno da bi se video TrailRenderer

    public GameObject carDriveSound;//Zvuk automobia

    public AudioSource stuntWithEnemySound;//Svuk stunt

    public Camera mainCamera;//Main kamera

    public Camera backCamera;//Back kamera

    public Animator[] whellAnimation;//Lista svih Animator komponenti koje zelim da pokrenem u mom slucaju okretanuje tockova,mogao sam i jedan po jedan Wheel da dodam ovako je brze samo ,sve moze da se uradi na vise nacina ,stvar je u tome samo kako znas umes kapiras i da li te mrzi XD

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        inGameSettings.SetActive(false);//U Start je inGameSetting false
    }


    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Provjera dodatnog ubrzanja pomoću tipke Shift
        if (Input.GetKey(KeyCode.LeftShift))//Ako je pritisnut LeftShitft
        {
            moveInput *= shiftSpeedMultiplier;
            nitro.SetActive(true);//Nitro je true i bice aktivan zvuk Nitro i takodje Trail Rendere
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))//Ako su ptitisnuti W i S 
        {
            carDriveSound.SetActive(true);//Aktiviraj zvuk  automobuila

            for (int i = 0; i < whellAnimation.Length; i++)//for petrlja da bih prosao kroz sve elemente iz liste 
            {
                whellAnimation[i].enabled = true;//Animator na Wheel je aktivan dok je pritisnuto W ili S
            }
        }
        else
        {
            for (int i = 0; i < whellAnimation.Length; i++)//for petrlja da bih prosao kroz sve elemente iz liste 
            {
                whellAnimation[i].enabled = false;//Animator na Wheel nije aktivan jer se Player ne krece
            }

            nitro.SetActive(false);
            carDriveSound.SetActive(false);
        }

        // Kretanje naprijed i natrag
        Vector3 movement = transform.forward * moveInput * speed * Time.deltaTime;

        // Provjeri postoji li prepreka ispred objekta
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                // Objekt neće ići naprijed jer postoji zid
                movement = Vector3.zero;
            }
        }

        rb.MovePosition(rb.position + movement);

        // Rotacija automobila
        Quaternion rotation = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.deltaTime, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    public void Update()
    {
        //Kamera
        if (Input.GetKey(KeyCode.V))//Ako je pritisnuto V i drzi se ,Main kamera ce biti iskljucena a bice ukljucena Back kamera
        {
            mainCamera.enabled = false;
            backCamera.enabled = true;
        }
        //Otvori podesavanja i stavi vreme na 0
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            inGameSettings.SetActive(true);//kaze samo da je active game object ,isto to je moglo i na drugaciji nacin tipa umesto GameObject InGameSetting,motao sam da stavim Image InGameSettings pa unutar Update samo InGameSettings.enabled = true;
            Time.timeScale = 0f;//vreme na 0
        }
        //Kamera
        else//Ako je nije  pritisnuto V i drzi se ,Main kamera ce biti ukljucena a bice iskljcuena Back kamera
        {
            mainCamera.enabled = true;
            backCamera.enabled = false;
        }
    }

    public void CloseInGameSettings()
    {
        inGameSettings.SetActive(false);//Stavi na false inGameSettings jer je ugasen 
        Time.timeScale = 1f;//Vreme krece jer nije vise aktivana inSettingsImage
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainScene");//Otvori scenu MainScene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wave1" || other.tag == "Wave2" || other.tag == "WaveBoss")//Ako colide sa objektima sa tim tagom aktivira se Stunt sound 
        {
            stuntWithEnemySound.Play();
        }
    }
}
