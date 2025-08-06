using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject mainMenuPanel;        
    public GameObject settingsPanel;        
    
    [Header("Audio")]
    public AudioClip buttonClickSound;      
    private AudioSource audioSource;       

    [Header("Settings Sliders")]
    public Slider musicVolumeSlider;        
    public Slider sfxVolumeSlider;          

    private void Start()
    {
        
       if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        }

        
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusicClip);
        } 
    }

   
    public void StartGame()
    {
        PlayButtonSound();
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Level1"); 
    }

    
    public void OpenSettings()
    {
        PlayButtonSound();
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    
    public void CloseSettings()
    {
        PlayButtonSound();
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    
    public void SetMusicVolume()
    {
        if (musicVolumeSlider != null && AudioManager.Instance != null)
        {
            float volume = musicVolumeSlider.value;
            AudioManager.Instance.SetMusicVolume(volume);
            Debug.Log($"🔊 Musica: {volume:F2}");
        }
    }


    public void SetSFXVolume()
    {
         if (sfxVolumeSlider != null && AudioManager.Instance != null)
        {
            float volume = sfxVolumeSlider.value;
            AudioManager.Instance.SetSFXVolume(volume);
            Debug.Log($"🔊 SFX: {volume:F2}");
        }
    }


    public void QuitGame()
    {
        PlayButtonSound();
        
        #if UNITY_EDITOR
            Debug.Log("Uscita dal gioco (Editor)");
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    
    private void PlayButtonSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        } 
    }
}