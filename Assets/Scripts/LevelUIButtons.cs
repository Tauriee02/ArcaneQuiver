using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIButtons : MonoBehaviour
{
    public void OnNextLevelButton()
    {
        LevelManager.Instance.LoadNextLevel();
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