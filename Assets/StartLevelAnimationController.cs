using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelAnimationController : MonoBehaviour
{
    public GameObject startLevelImage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StopStartLevelImage());
    }

    IEnumerator StopStartLevelImage()
    { 
        yield return new WaitForSeconds(3);
        startLevelImage.SetActive(false);
    }
}
