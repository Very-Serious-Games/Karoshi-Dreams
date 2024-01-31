using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    [Header("Light Settings")]
    public float threshold = 0.01f;
    public float flickerAmount = 0.2f;

    [Header("Audio Settings")]
    public AudioController audioController;

    private Light lightComponent;
    private float originalIntensity;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light>();
        originalIntensity = 35;
        
        audioController.PlayAudio("fluorescent");
    }

    // Update is called once per frame
    void Update()
    {

        if (Random.value < threshold)
        {
            StartCoroutine(TurnOnLight());
            StartCoroutine(TurnOffLight());
        }
    }

    private IEnumerator TurnOnLight()
    {
        float duration = Random.Range(0.1f, 0.2f);
        float elapsed = 0;

        int randomAudio = Random.Range(1, 2);

        if (randomAudio == 1) {
            audioController.PlayAudio("flickering_lights");
        } else if (randomAudio == 2) {
            audioController.PlayAudio("flickering_lights_2");
        }

        while (elapsed < duration)
        {
            float intensity = Mathf.Lerp(0, originalIntensity, elapsed / duration);
            lightComponent.intensity = intensity + Random.Range(-flickerAmount, flickerAmount);
            elapsed += Time.deltaTime;

            yield return null;
        }

        lightComponent.intensity = originalIntensity;
    }

    private IEnumerator TurnOffLight()
    {
        float duration = Random.Range(0.1f, 0.2f);
        float elapsed = 0;

        while (elapsed < duration)
        {
            lightComponent.intensity = Mathf.Lerp(originalIntensity, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        lightComponent.intensity = 0;
    }
}