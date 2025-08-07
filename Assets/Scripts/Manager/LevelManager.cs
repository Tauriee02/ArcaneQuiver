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

    
    [SerializeField] private GameObject nextLevelUI;         
    [SerializeField] private Text enemiesKilledText;           
    [SerializeField] private Text timeSurvivedText;            
     

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetLevelState();
        
        ResetUIReferences();
    }
    
    private void ResetLevelState()
    {
        enemiesKilled = 0;
        timeSurvived = 0f;
        levelCompleted = false;
    }

    private void ResetUIReferences()
    {

        
        if (nextLevelUI == null)
        {
            
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                Transform panel = canvas.transform.Find("NextLevelUI");
                if (panel != null)
                {
                    nextLevelUI = panel.gameObject;
                    Debug.Log("✅ nextLevelUI trovato in Canvas");
                }
            }
        }

        
        if (nextLevelUI != null)
        {
            
            Text[] texts = nextLevelUI.GetComponentsInChildren<Text>(true);
            foreach (Text t in texts)
            {
                if (t.name == "EnemiesKilledText" && enemiesKilledText == null)
                {
                    enemiesKilledText = t;
                }
                else if (t.name == "TimeSurvivedText" && timeSurvivedText == null)
                {
                    timeSurvivedText = t;
                }
            }
        }

        
        if (nextLevelUI == null)
        {
            Debug.LogError("❌ nextLevelUI non trovato nella scena!");
        }
        else
        {
            nextLevelUI.SetActive(false); 
        }
    }

    private void Start()
    {

        if (AudioManager.Instance != null && AudioManager.Instance.gameplayMusicClip != null)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusicClip);
        }
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
         PlayerHealth player = FindObjectOfType<PlayerHealth>();
        if (player != null)
        {
            player.SaveCurrentHealth();
            Debug.Log($"💾 Vita salvata prima di andare a LevelSelect: {player.CurrentHealth}/{player.maxHealth}");
        }
        enemiesKilled = 0;
        timeSurvived = 0f;
        levelCompleted = false;

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("LevelSelect");
        }
        else
        {
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

        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        if (player != null)
        {
            player.SaveCurrentHealth();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}