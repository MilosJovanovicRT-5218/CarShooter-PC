using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxManager : MonoBehaviour
{
    public Material skyboxMaterial; // Reference na materijal skyboxa za ovu scenu

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial; // Postavi skybox materijal za ovu scenu
    }
}
