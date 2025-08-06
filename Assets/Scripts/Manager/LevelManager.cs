using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Serve per accedere ai componenti UI (Text, Image, ecc.)

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    
    public int enemiesKilled = 0;
    public int enemiesToKill = 15;

    
    public GameObject nextLevelUI;         
    public Text enemiesKilledText;           
    public Text timeSurvivedText;            
     

    private float timeSurvived = 0f;
    private bool levelCompleted = false;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }

    private void Start()
    {

        timeSurvived = 0f;
        levelCompleted = false;
        AudioManager.Instance.PlayGameStart();
        AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusicClip);
    }

    private void Update()
    {
       
        if (!levelCompleted && nextLevelUI != null && !nextLevelUI.activeSelf)
        {
            timeSurvived += Time.deltaTime;
        }
    }

    
    public void EnemyKilled()
    {
        enemiesKilled++;

        
        if (enemiesKilled >= enemiesToKill && !levelCompleted)
        {
            levelCompleted = true;
            ShowNextLevelUI();
        }
    }

   
    public void ShowNextLevelUI()
    {
        Time.timeScale = 0f; 

        if (nextLevelUI != null)
        {
            nextLevelUI.SetActive(true);

            
            if (enemiesKilledText != null)
                enemiesKilledText.text = $"Nemici Uccisi: {enemiesKilled}/{enemiesToKill}";

            if (timeSurvivedText != null)
                timeSurvivedText.text = $"Tempo: {timeSurvived:F1}s";
        }
        else
        {
            Debug.LogError("nextLevelUI non assegnato in LevelManager!");
        }
    }

    
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; 
        enemiesKilled = 0;
        timeSurvived = 0f;
        levelCompleted = false;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (currentIndex + 1 < sceneCount)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.Log("Hai completato tutti i livelli!");
            LoadMainMenu();
        }
    }

   
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}