using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject pauseMenuUI;
    public GameObject crossbarImage;

    [Header("Scene Settings")]
    public SceneSwitcher sceneSwitcher;
    public string mainMenuSceneName;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        crossbarImage.SetActive(true); // Enable the crossbar image
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        crossbarImage.SetActive(false); // Disable the crossbar image
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }

    public void MainMenu()
    {
        Resume();
        sceneSwitcher.SwitchScene(mainMenuSceneName);
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }
}