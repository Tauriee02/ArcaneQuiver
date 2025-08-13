using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    
    public int enemiesKilled = 0;
    public int enemiesToKill = 15;

    
    [SerializeField] private GameObject nextLevelUI;         
    [SerializeField] private TMP_Text enemiesKilledText;           
    [SerializeField] private TMP_Text timeSurvivedText;            
     

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
        string[] gameLevelNames = { "Level1", "Level2", "Level3" };
        bool isGameLevel = System.Array.Exists(gameLevelNames, levelName => levelName == scene.name);

        if (isGameLevel)
        {
            Debug.Log($"LevelManager: Caricato livello di gioco '{scene.name}'. Inizializzo UI.");

            ResetLevelState();

            switch (scene.name)
            {
                case "Level1":
                    enemiesToKill = 15;
                    break;
                case "Level2":
                    enemiesToKill = 20;
                    break;
                case "Level3":
                    enemiesToKill = 1;
                    break;
                default:
                    enemiesToKill = 15;
                    break;
            }

            Debug.Log($"Obiettivo nemici impostato a {enemiesToKill} per {scene.name}");

            ResetUIReferences();
        }
        else
        {
            Debug.Log($"LevelManager: '{scene.name}' non è un livello di gioco. Disabilito UI.");
            nextLevelUI = null;
            enemiesKilledText = null;
            timeSurvivedText = null;
        }
    }
    

    private void ResetLevelState()
    {
        enemiesKilled = 0;
        timeSurvived = 0f;
        levelCompleted = false;
    }

    private void ResetUIReferences()
    {
        nextLevelUI = null;
        enemiesKilledText = null;
        timeSurvivedText = null;

        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Canvas non trovato nella scena!");
            return;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        GameObject panel = null;

        if (sceneName == "Level3")
        {
            Transform finalPanel = FindInParentsOrChildren(canvas.transform, "FinalLevelUI");
            if (finalPanel != null)
            {
                panel = finalPanel.gameObject;
                Debug.Log("FinalLevelUI trovato!");
            }
        }
        else
        {
            Transform nextPanel = FindInParentsOrChildren(canvas.transform, "NextLevelUI");
            if (nextPanel != null)
            {
                panel = nextPanel.gameObject;
                Debug.Log("NextLevelUI trovato!");
            }
        }

        if (panel != null)
        {
            TMP_Text[] texts = panel.GetComponentsInChildren<TMP_Text>(true);
            foreach (TMP_Text t in texts)
            {
                if (t.name == "EnemiesKilledText") enemiesKilledText = t;
                else if (t.name == "TimeSurvivedText") timeSurvivedText = t;
            }

            nextLevelUI = panel;
            nextLevelUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Nessun pannello UI trovato (né NextLevelUI né FinalLevelUI)!");
        }
    }

    private Transform FindInParentsOrChildren(Transform parent, string name)
    {
        if (parent.name == name)
            return parent;

        foreach (Transform child in parent)
        {
            Transform found = FindInParentsOrChildren(child, name);
            if (found != null) return found;
        }
        return null;
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
        Debug.Log($"Contatore nemici uccisi: {enemiesKilled}");

        if (enemiesKilled >= enemiesToKill && !levelCompleted)
        {
            levelCompleted = true;
            ShowNextLevelUI();
        }
        
    }

   
    public void ShowNextLevelUI()
    {
        Time.timeScale = 0f; 
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("NextLevelBuildIndex", nextLevelIndex);
            PlayerPrefs.Save();
            Debug.Log($"Prossimo livello salvato: indice {nextLevelIndex}");
        }
        else
        {
            PlayerPrefs.SetInt("NextLevelBuildIndex", -1);
            PlayerPrefs.Save();
        }

        if (nextLevelUI == null || enemiesKilledText == null || timeSurvivedText == null)
        {
            ResetUIReferences();
        }

        if (nextLevelUI != null)
        {
            nextLevelUI.SetActive(true);

            if (enemiesKilledText != null)
                enemiesKilledText.text = $"Nemici Uccisi: {enemiesKilled}/{enemiesToKill}";
            else
                Debug.LogWarning("enemiesKilledText non trovato!");

            if (timeSurvivedText != null)
                timeSurvivedText.text = $"Tempo: {timeSurvived:F1}s";
            else
                Debug.LogWarning("timeSurvivedText non trovato!");
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
            Debug.Log($"Player trovato: {player.CurrentHealth}/{player.maxHealth}. Salvataggio...");
            player.SaveCurrentHealth();
        }
        else
        {
            Debug.LogError("Player non trovato! Impossibile salvare la vita.");
        }

        ResetLevelState();

        SceneManager.LoadScene("LevelSelect");
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