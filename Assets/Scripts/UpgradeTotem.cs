using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeTotem : MonoBehaviour
{
    [Header("Upgrade Options")]
    public GameObject upgradePanel; 
    public string nextLevelName = "Level2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (upgradePanel != null)
            {
                upgradePanel.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log("🎯 Totem attivato! Scegli un potenziamento.");
            }
            else
            {
                Debug.LogError("❌ upgradePanel non assegnato!");
            }
        }
    }

    
    public void AddMaxHealth()
    {
        
        int currentBonus = PlayerPrefs.GetInt("HealthBonus", 0);
        PlayerPrefs.SetInt("HealthBonus", currentBonus + 1);
        
        
        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        if (player != null)
        {
            player.IncreaseMaxHealth(1); 
            player.SaveCurrentHealth();
        }

        ContinueToNextLevel();
    }

    public void AddExtraShot()
    {
         PlayerPrefs.SetInt("HasDoubleShot", 1);
        PlayerPrefs.Save();

        Debug.Log("⚡ Doppio tiro abilitato e salvato!");
        ContinueToNextLevel();
    }

    public void ContinueToNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevelName);
    }
}
