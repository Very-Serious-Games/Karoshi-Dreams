using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Volume globalVolume; // Assign this in the Inspector
    private Vignette vignette;

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        // Get the Vignette effect from the global volume
        if (globalVolume.profile.TryGet(out vignette))
        {
            // Increase the intensity of the Vignette effect to 1
            while (vignette.intensity.value < 1f)
            {
                vignette.intensity.value += Time.deltaTime * 0.3f; // Adjust the speed of the transition by changing this value
                yield return null;
            }

            // Once the intensity is 1, switch scenes
            SceneManager.LoadScene(sceneName);
            Debug.Log("Switched to scene: " + sceneName);
        }
    }

    public void CloseGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}