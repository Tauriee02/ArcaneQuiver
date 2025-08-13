using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;
    
    [Header("Volume Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool isPaused = false;

    private void Start()
    {
        
        if (pausePanel != null)
            pausePanel.SetActive(false);

        SetupButtonEvents();
       
        LoadVolumeSettings();
    }

    private void SetupButtonEvents()
    {
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
        
        Debug.Log("⏸️ Gioco in pausa");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
        
        Debug.Log("▶️ Gioco ripreso");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void SetMusicVolume()
    {
        if (musicSlider != null)
        {
            float volume = musicSlider.value;
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMusicVolume(volume);
            }
            PlayerPrefs.SetFloat("MusicVolume", volume); 
            Debug.Log($"🎵 Volume Musica: {volume:F2}");
        }
    }

    public void SetSfxVolume()
    {
        if (sfxSlider != null)
        {
            float volume = sfxSlider.value;
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSFXVolume(volume);
            }
            PlayerPrefs.SetFloat("SfxVolume", volume); 
            Debug.Log($"🔊 Volume SFX: {volume:F2}");
        }
    }

    private void LoadVolumeSettings()
    {
        if (musicSlider != null)
        {
            float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.value = savedMusic;
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMusicVolume(savedMusic);
            }
        }

        if (sfxSlider != null)
        {
            float savedSfx = PlayerPrefs.GetFloat("SfxVolume", 1f);
            sfxSlider.value = savedSfx;
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSFXVolume(savedSfx);
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}