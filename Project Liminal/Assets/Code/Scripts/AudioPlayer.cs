using UnityEngine;
using UnityEngine.UI;

public class PlayAudioButton : MonoBehaviour
{
    public AudioController audioController; // Reference to the AudioController
    public string audioKey; // Key of the audio to play
    
    // Play the audio
    public void PlayAudio()
    {
        audioController.PlayAudio(audioKey);
    }
}