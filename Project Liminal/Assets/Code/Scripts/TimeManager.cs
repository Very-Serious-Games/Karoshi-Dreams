using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher;

    private float elapsedTime = 0f;

    bool isNightmare = false;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        switch (GlobalVariables.rounds)
        {
            case 0:
                if (elapsedTime > 100f)
                {
                    if(!isNightmare){
                        sceneSwitcher.SwitchScene("NightmareScene");
                        isNightmare = true;
                        GlobalVariables.rounds++;
                    }   
                }
                break;
            case 1:
                if (elapsedTime > Random.Range(20f, 40f))
                {
                    if(!isNightmare){
                        sceneSwitcher.SwitchScene("NightmareScene");
                        isNightmare = true;
                        GlobalVariables.rounds++;
                    }
                }
                break;
            case 2:
                break;
        }
    }
}