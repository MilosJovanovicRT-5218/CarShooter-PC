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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inGameSettings.SetActive(true);
            Time.timeScale = 0f;
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
}
