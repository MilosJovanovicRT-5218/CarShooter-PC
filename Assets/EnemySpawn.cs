using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefabWawe1;
    public GameObject enemyPrefabWawe2;
    public Transform[] spawnPoints;

    public GameObject enemyBoss;
    public Transform Boss;

    public float firstWaveStartTime = 10f;
    public float spawnTimeWave1 = 3f;
    public float wave1Duration = 60f;
    public float pauseTime = 10f;
    public float pauseTime2 = 10f;
    public float spawnTimeWave2 = 3f;
    public float wave2Duration = 60f;

    public float waveBoss = 120f;

    public Text time;

    private float firstWaveRemainingTime;

    private float pauseRemainingTime;
    private float pauseRemainingTime2;

    private float timeRemainingWave1;
    private float timeRemainingWave2;

    private float timeBossRemaining;

    public void Update()
    {
        if (firstWaveRemainingTime > 0f)
        {
            firstWaveRemainingTime -= Time.deltaTime;
            time.text = "Start in " + Mathf.RoundToInt(firstWaveRemainingTime).ToString() + " seconds";
        }
        else if (timeRemainingWave1 > 0f)
        {
            timeRemainingWave1 -= Time.deltaTime;
            time.text = "Wave 1 ends in " + Mathf.RoundToInt(timeRemainingWave1).ToString() + " seconds";
        }
        else if (pauseRemainingTime > 0f)
        {
            pauseRemainingTime -= Time.deltaTime;
            time.text = "Pause " + Mathf.RoundToInt(pauseRemainingTime).ToString() + " seconds";
        }
        else if (timeRemainingWave2 > 0f)
        {
            timeRemainingWave2 -= Time.deltaTime;
            time.text = "Wave 2 ends in " + Mathf.RoundToInt(timeRemainingWave2).ToString() + " seconds";
        }
        else if (pauseRemainingTime2 > 0f)
        {
            pauseRemainingTime2 -= Time.deltaTime;
            time.text = "Pause,remaining Boss " + Mathf.RoundToInt(pauseRemainingTime2).ToString() + " seconds";
        }
        else if (timeBossRemaining > 0f)
        {
            timeBossRemaining -= Time.deltaTime;
            time.text = "Boss " + Mathf.RoundToInt(timeBossRemaining).ToString() + " seconds";
        }
    }

    void Start()
    {
        // Invoke Wave 1
        InvokeRepeating("SpawnWave1Enemy", firstWaveStartTime, spawnTimeWave1);
        Invoke("StopWave1", firstWaveStartTime + wave1Duration - 10f + pauseTime);

        // Invoke Wave 2
        float wave2Start = firstWaveStartTime - 10f + wave1Duration + pauseTime + 10f;
        InvokeRepeating("SpawnWave2Enemy", wave2Start, spawnTimeWave2);
        Invoke("StopWave2", wave2Start + wave2Duration);//-10f

        // Invoke Boss Wave after a pause
        float bossWaveStart = wave2Start + wave2Duration + pauseTime2;
        Invoke("SpawnBoss", bossWaveStart);
        //Invoke("StopBoss", bossWaveStart + waveBoss);
        //
        firstWaveRemainingTime = firstWaveStartTime;
        timeRemainingWave1 = wave1Duration;
        pauseRemainingTime = pauseTime;
        timeRemainingWave2 = wave2Duration;
        pauseRemainingTime2 = pauseTime2;
        timeBossRemaining = waveBoss;
    }

    void SpawnWave1Enemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(enemyPrefabWawe1, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnWave2Enemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(enemyPrefabWawe2, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnBoss()
    {
        Instantiate(enemyBoss, Boss.position, Boss.rotation);
    }

    void StopWave1()
    {
        CancelInvoke("SpawnWave1Enemy");
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Wave1");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }

    void StopWave2()
    {
        CancelInvoke("SpawnWave2Enemy");
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Wave2");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }

    void StopBoss()
    {
        CancelInvoke("SpawnBoss");
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("WaveBoss");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }
}
