using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeRoundManager : MonoBehaviour
{
    public ObjetosVariables objetosVariables;

    public GameObject SceneObjects;

    // Start is called before the first frame update
    void Start()
    {
        switch (GlobalVariables.rounds)
        {
            case 0:
                break;
            case 1:
                objetosVariables.randomDeactivation();
                break;
            case 2:
                objetosVariables.RandomRotation();
                break;
            case 3:
                objetosVariables.RandomScale();
                break;
            case 4:
                
                break;
            case 5:
                for (int i = 0; i < SceneObjects.transform.childCount; i++)
                {
                    Transform child = SceneObjects.transform.GetChild(i);
                    if(child.gameObject.name != "pc" && child.gameObject.name != "Keyboard" && child.gameObject.name != "screen")
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                RenderSettings.fogColor = Color.white;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
