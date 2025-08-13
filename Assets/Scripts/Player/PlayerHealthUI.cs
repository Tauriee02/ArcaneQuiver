using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("UI Hearts")]
    public Image[] hearts; 

    [Header("Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private PlayerHealth playerHealth;

    private void Start()
    {
        StartCoroutine(FindPlayerWithDelay());
    }

    private IEnumerator FindPlayerWithDelay()
    {
        
        yield return new WaitForSeconds(0.5f);

        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            Debug.Log("✅ PlayerHealthUI: PlayerHealth trovato!");
            UpdateHearts();
            
        }
        else
        {
            Debug.LogError("❌ PlayerHealthUI: PlayerHealth non trovato neanche dopo ritardo!");
            
            yield return new WaitForSeconds(1f);
            playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("✅ Trovato in ritardo!");
                UpdateHearts();
            }
            else
            {
                Debug.LogError("❌ Ancora non trovato. Assicurati che il player abbia PlayerHealth.");
            }
        }
    }


    private void Update()
    {
        
        UpdateHearts();
    }

    
    void UpdateHearts()
    {
        if (playerHealth == null) return;
        
        int currentMax = playerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.CurrentHealth)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
