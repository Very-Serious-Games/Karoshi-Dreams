using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    public float threshold = 0.01f;
    public float flickerAmount = 0.2f;

    private Light lightComponent;
    private float originalIntensity;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light>();
        originalIntensity = 35;
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
        float duration = Random.Range(0.1f, 0.5f);
        float elapsed = 0;

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
        float duration = Random.Range(0.1f, 0.5f);
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