using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
     public int enemiesKilled = 0;
    public int enemiesToKill = 15;

    public GameObject nextLevelUI;

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
        }
    }
    
     public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemiesToKill)
        {
            ShowNextLevelUI();
        }
    }

    public void ShowNextLevelUI()
    {
        Time.timeScale = 0f; 
        
        if (nextLevelUI != null)
        {
            nextLevelUI.SetActive(true);
        }

        
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        enemiesKilled = 0;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.Log("Hai completato tutti i livelli!");
        }
    }

}
