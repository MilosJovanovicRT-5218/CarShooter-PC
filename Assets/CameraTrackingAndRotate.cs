using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingAndRotate : MonoBehaviour
{
    public Transform player; // Referenca na igraèa
    public float rotationSpeed = 5f; // Brzina rotacije kamere
    public float distance = 5f; // Udaljenost kamere od igraèa

    private float mouseY; // Trenutna koordinata miša po Y osi
  
    public float desiredHeight = 5.13f;
    private void LateUpdate()
    {
        // Dobivanje trenutnog pomaka miša po X osi
        mouseY += Input.GetAxis("Mouse X") * rotationSpeed;
        // Rotacija kamere oko igraèa samo na Y osi
        Quaternion rotation = Quaternion.Euler(/*mouseY*/0, mouseY, 0);

        // Izraèunavanje nove pozicije kamere u odnosu na rotaciju, udaljenost i poziciju igraèa
        Vector3 cameraPosition = player.position - (rotation * Vector3.forward) * distance + Vector3.up * desiredHeight;
     
        // Postavljanje nove pozicije kamere
        transform.position = cameraPosition;

        // Postavljanje kamere da uvijek gleda prema igraèu
        transform.LookAt(player.position);
    }
}
