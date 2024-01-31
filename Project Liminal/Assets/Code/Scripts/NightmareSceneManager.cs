using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightmareSceneManager : MonoBehaviour
{
    public UniversalRendererData rendererData;

    // Start is called before the first frame update
    void Start()
    {
        rendererData.rendererFeatures[2].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
