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
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = 0.7f;  

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = 1.0f;    
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
        if (musicVolumeSlider != null)
        {
            
            float volume = musicVolumeSlider.value;
            Debug.Log($"Volume Musica impostato a: {volume:F2}");
            // AudioManager.Instance.SetMusicVolume(volume); // Opzionale in futuro
        }
    }


    public void SetSFXVolume()
    {
        if (sfxVolumeSlider != null)
        {
            float volume = sfxVolumeSlider.value;
            Debug.Log($"Volume SFX impostato a: {volume:F2}");
            // AudioManager.Instance.SetSFXVolume(volume);
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
        AudioManager.Instance.PlayButtonClick(); 
    }
}