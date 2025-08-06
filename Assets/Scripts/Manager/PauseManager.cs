using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = 0.7f;

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = 1.0f;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        //AudioManager.Instance.SetMusicVolume(volume)
        Debug.Log($"Volume musica: {volume:F2}");
    }

    public void SetSFXVolume()
    {
        float volume = sfxVolumeSlider.value;
        // AudioManager.Instance.SetSFXVolume(volume)
        Debug.Log($"Volume SFX: {volume:F2}");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}