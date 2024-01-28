using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SceneLoader : MonoBehaviour
{
    public Volume globalVolume; // Assign this in the Inspector
    private Vignette vignette;

    void Start()
    {
        StartCoroutine(TransitionFromScene());
    }

    private IEnumerator TransitionFromScene()
    {
        // Get the Vignette effect from the global volume
        if (globalVolume.profile.TryGet(out vignette))
        {
            // Set the initial intensity of the Vignette effect to 1
            vignette.intensity.value = 1f;

            // Decrease the intensity of the Vignette effect to 0
            while (vignette.intensity.value > 0f)
            {
                vignette.intensity.value -= Time.deltaTime * 0.3f; // Adjust the speed of the transition by changing this value
                yield return null;
            }
        }
    }
}