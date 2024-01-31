using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AwakeSceneManager : MonoBehaviour
{
    public UniversalRendererData rendererData;

    // Start is called before the first frame update
    void Start()
    {
        if(GlobalVariables.rounds == 4)
        {
            rendererData.rendererFeatures[2].SetActive(true);
        }
        else
        {
            rendererData.rendererFeatures[2].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
