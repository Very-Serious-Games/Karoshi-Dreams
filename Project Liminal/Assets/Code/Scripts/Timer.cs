using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float delayTime = 3f; // Delay time in seconds
    private float countdown; // Countdown timer

    // Start is called before the first frame update
    void Start()
    {
        countdown = delayTime; // Set the initial countdown value
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime; // Decrease the countdown by the elapsed time
        }
        else
        {
            // Timer has finished, do something here
            // Example: Activate an object, trigger an event, etc.
        }
    }
}
