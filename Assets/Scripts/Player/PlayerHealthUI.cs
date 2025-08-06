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
        
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("❌ PlayerHealth non trovato!");
            return;
        }

        
        UpdateHearts();
    }

    private void Update()
    {
        
        UpdateHearts();
    }

    
    void UpdateHearts()
    {
        if (playerHealth == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.CurrentHealth)
            {
                hearts[i].sprite = fullHeart; 
            }
            else
            {
                hearts[i].sprite = emptyHeart; 
            }
        }
    }
}
