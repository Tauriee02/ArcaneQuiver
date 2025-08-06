using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public float delayBeforeGameOver = 0.5f;

    private bool hasGameEnded = false;

    
    public void PlayerDied()
    {
        if (hasGameEnded) return;
        hasGameEnded = true;
        Time.timeScale = 0f; 

        Invoke(nameof(ShowGameOverPanel), delayBeforeGameOver);
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverPanel non assegnato in GameOverManager!");
        }
    }

   
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
