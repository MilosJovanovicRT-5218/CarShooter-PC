using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public string playerTag = "Player"; // Tag igra�evog objekta
    public float viewRange = 15f; // Udaljenost na kojoj se zvuk mo�e �uti

    private GameObject player; // Referenca na igra�ev objekt

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= viewRange)
        {
            GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }
}