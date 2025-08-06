using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public float delayBeforeGameOver = 0f;
    private bool hasGameEnded = false;

    
    public void PlayerDied()
    {
        if (hasGameEnded) return;
        hasGameEnded = true;
        Time.timeScale = 0f; 

        StartCoroutine(ShowAfterDelay());
    }

    IEnumerator ShowAfterDelay()
    {
           if (delayBeforeGameOver > 0f)
        {
            yield return new WaitForSecondsRealtime(delayBeforeGameOver);
        }
        ShowGameOverPanel();
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
