using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public string playerTag = "Player"; // Tag igraèevog objekta
    public float viewRange = 15f; // Udaljenost na kojoj se zvuk može èuti

    private GameObject player; // Referenca na igraèev objekt

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