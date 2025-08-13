using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIButtons : MonoBehaviour
{
    public void OnNextLevelButton()
    {
        int nextIndex = PlayerPrefs.GetInt("NextLevelBuildIndex", -1);

        if (nextIndex >= 0 && nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextIndex);
            Debug.Log($"Caricato livello con indice: {nextIndex}");
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnRestartButton()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void OnMainMenuButton()
    {
        LevelManager.Instance.LoadMainMenu();
    }
}