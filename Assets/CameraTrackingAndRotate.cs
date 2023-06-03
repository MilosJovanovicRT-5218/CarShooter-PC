using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingAndRotate : MonoBehaviour
{
    public Transform player; // Referenca na igra�a
    public float rotationSpeed = 5f; // Brzina rotacije kamere
    public float distance = 5f; // Udaljenost kamere od igra�a

    private float mouseY; // Trenutna koordinata mi�a po Y osi
  
    public float desiredHeight = 5.13f;
    private void LateUpdate()
    {
        // Dobivanje trenutnog pomaka mi�a po X osi
        mouseY += Input.GetAxis("Mouse X") * rotationSpeed;
        // Rotacija kamere oko igra�a samo na Y osi
        Quaternion rotation = Quaternion.Euler(/*mouseY*/0, mouseY, 0);

        // Izra�unavanje nove pozicije kamere u odnosu na rotaciju, udaljenost i poziciju igra�a
        Vector3 cameraPosition = player.position - (rotation * Vector3.forward) * distance + Vector3.up * desiredHeight;
     
        // Postavljanje nove pozicije kamere
        transform.position = cameraPosition;

        // Postavljanje kamere da uvijek gleda prema igra�u
        transform.LookAt(player.position);
    }
}
