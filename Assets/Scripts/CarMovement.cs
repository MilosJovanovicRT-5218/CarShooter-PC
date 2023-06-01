using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float shiftSpeedMultiplier = 3;

    private Rigidbody rb;

    public GameObject inGameSettings;

    public GameObject nitro;

    public GameObject carDriveSound;

    public AudioSource stuntWithEnemySound;

    public Camera mainCamera;

    public Camera backCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        inGameSettings.SetActive(false);
    }


    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Provjera dodatnog ubrzanja pomoću tipke Shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput *= shiftSpeedMultiplier;
            nitro.SetActive(true);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            carDriveSound.SetActive(true);
        }
        else
        {
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
        if (Input.GetKey(KeyCode.V))
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
        else
        {
            mainCamera.enabled = true;
            backCamera.enabled = false;
        }
    }

    public void CloseInGameSettings()
    {
        inGameSettings.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wave1" || other.tag == "Wave2" || other.tag == "WaveBoss")//Ako colide sa objektima sa tim tagom aktivira se Stunt sound 
        {
            stuntWithEnemySound.Play();
        }
    }
}
