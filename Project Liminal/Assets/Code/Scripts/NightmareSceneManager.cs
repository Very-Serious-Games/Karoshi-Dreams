using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightmareSceneManager : MonoBehaviour
{
    public UniversalRendererData rendererData;

    public AudioController audioController;

    private float audioDelay = 10f;
    private float timer = 0f;
    private string[] audioClips = { "sonido-demon-1", "sonido-demon-2" };

    // Start is called before the first frame update
    void Start()
    {
        rendererData.rendererFeatures[2].SetActive(true);
        audioController.PlayAudio("nightmare_background");

        timer = Random.Range(10f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // Check if the timer has reached zero
        if (timer <= 0f)
        {
            // Play a random audio
            int randomIndex = Random.Range(0, audioClips.Length);
            audioController.PlayAudio(audioClips[randomIndex]);

            // Reset the timer
            timer = Random.Range(10f, 20f);
        }
    }
}
