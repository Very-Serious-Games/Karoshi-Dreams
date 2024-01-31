using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject settingsMenu;
    public Button[] otherButtons;
    public Slider volumeSlider; // Slider for volume
    public Slider sensitivitySlider; // Slider for mouse sensitivity
    public TextMeshProUGUI volumeText; // Text for volume
    public TextMeshProUGUI sensitivityText; // Text for mouse sensitivity

    [Header("Audio Settings")]
    public AudioController audioController; // Reference to the AudioController

    private float initialVolume;
    private float initialSensitivity;

    private void Start()
    {
        // Load saved settings
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);

        // Update text
        UpdateVolumeText(volumeSlider.value);
        UpdateSensitivityText(sensitivitySlider.value);

        // Add listener for when the volume slider changes
        volumeSlider.onValueChanged.AddListener(value =>
        {
            UpdateVolumeText(value);
            SetAllAudioVolumes(value);
        });

        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivityText);
    }

    private void SetAllAudioVolumes(float volume)
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = volume;
        }
    }

    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);

        // Disable other buttons
        foreach (Button button in otherButtons)
        {
            button.interactable = false;
            button.gameObject.SetActive(false);
        }

        // Store initial settings
        initialVolume = volumeSlider.value;
        initialSensitivity = sensitivitySlider.value;
    }

    public void SaveAndHideSettingsMenu()
    {
        settingsMenu.SetActive(false);

        // Enable other buttons
        foreach (Button button in otherButtons)
        {
            button.interactable = true;
            button.gameObject.SetActive(true);
        }

        // Save settings
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        PlayerPrefs.Save();
    }

    public void CancelAndHideSettingsMenu()
    {
        settingsMenu.SetActive(false);

        // Enable other buttons
        foreach (Button button in otherButtons)
        {
            button.interactable = true;
            button.gameObject.SetActive(true);
        }

        // Restore initial settings
        volumeSlider.value = initialVolume;
        sensitivitySlider.value = initialSensitivity;
        UpdateVolumeText(initialVolume);
        UpdateSensitivityText(initialSensitivity);
        SetAllAudioVolumes(initialVolume);
    }

    private void UpdateVolumeText(float volume)
    {
        volumeText.text = volume.ToString("0.0");
    }

    private void UpdateSensitivityText(float sensitivity)
    {
        sensitivityText.text = sensitivity.ToString("0.0");
    }
}