using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeTotem : MonoBehaviour
{
    [Header("Upgrade Options")]
    public GameObject upgradePanel; 

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

    void ContinueToNextLevel()
    {
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log($"✅ Caricato il livello successivo: indice {nextSceneIndex}");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
