using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioEntry
{
    public string key;
    public AudioSource audioSource;
}

public class AudioController : MonoBehaviour {

    [SerializeField]
    private List<AudioEntry> audioEntries = new List<AudioEntry>();

    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        foreach (var entry in audioEntries)
        {
            AddAudioSource(entry.key, entry.audioSource);
        }
    }

    public void AddAudioSource(string key, AudioSource audioSource)
    {
        if (!audioSources.ContainsKey(key))
        {
            audioSources.Add(key, audioSource);
        }
    }

    public void PlayAudio(string key)
    {
        if (audioSources.ContainsKey(key))
        {
            audioSources[key].Play();
        }
    }

    public void StopAudio(string key)
    {
        if (audioSources.ContainsKey(key))
        {
            audioSources[key].Stop();
        }
    }

    public void PauseAudio(string key)
    {
        if (audioSources.ContainsKey(key))
        {
            audioSources[key].Pause();
        }
    }

    public void ResumeAudio(string key)
    {
        if (audioSources.ContainsKey(key))
        {
            audioSources[key].UnPause();
        }
    }

    public void SetVolume(string key, float volume)
    {
        if (audioSources.ContainsKey(key))
        {
            audioSources[key].volume = volume;
        }
    }
    
}