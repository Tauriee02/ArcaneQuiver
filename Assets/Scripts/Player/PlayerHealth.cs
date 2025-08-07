using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vita")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Riferimenti")]
    public GameObject gameOverManagerObj; // Opzionale: assegnazione manuale

    private GameOverManager gm;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    [Header("Feedback Visivo")]
    public float invincibilityDuration = 1f;
    private bool isInvincible = false;

    private void Awake()
    {
        SetupComponents();
        FindGameOverManager();

    }

    private void Start()
    {

        int healthBonus = PlayerPrefs.GetInt("HealthBonus", 0);

        maxHealth = 3 + healthBonus;
        int savedCurrent = PlayerPrefs.GetInt("SavedCurrentHealth", -1);

        if (savedCurrent >= 0 && savedCurrent <= maxHealth)
        {
            currentHealth = savedCurrent;
            Debug.Log($"❤️ Caricata vita salvata: {currentHealth}/{maxHealth}");
        }
        else
        {
            currentHealth = maxHealth;
             Debug.Log($"⚠️ Nessun valore valido di vita salvata. Riparto con: {currentHealth}/{maxHealth}");
        }

    }

    private void SetupComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();

        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer mancante su player!");
        if (animator == null)
            Debug.LogError("Animator mancante su player!");
        if (playerMovement == null)
            Debug.LogWarning("PlayerMovement non trovato (opzionale)");
        if (playerShooter == null)
            Debug.LogWarning("PlayerShooter non trovato (opzionale)");
    }

    private void FindGameOverManager()
    {
        if (gameOverManagerObj != null)
        {
            gm = gameOverManagerObj.GetComponent<GameOverManager>();
        }
        else
        {
            GameObject goObj = GameObject.Find("GameOverManager");
            if (goObj != null)
            {
                gm = goObj.GetComponent<GameOverManager>();
            }
        }

        if (gm == null)
        {
            Debug.LogError("❌ GameOverManager non trovato! Assicurati che esista nella scena.");
        }
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentHealth--;
        Debug.Log($"Player ha preso danno! Vita: {currentHealth}");

        isInvincible = true;
        StartCoroutine(FlashPlayer());
        Invoke(nameof(ResetInvincibility), invincibilityDuration);

        animator?.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ResetInvincibility()
    {
        isInvincible = false;
    }

    IEnumerator FlashPlayer()
    {
        if (spriteRenderer == null) yield break;

        int flashes = 6;
        for (int i = 0; i < flashes; i++)
        {
            spriteRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Die()
    {
        Debug.Log("Player morto! Avvio Game Over...");

        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPlayerDeath();
            AudioManager.Instance.StopMusic();
        }

        
        if (playerMovement != null) playerMovement.enabled = false;
        if (playerShooter != null) playerShooter.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;

        
        if (gm != null)
        {
            Debug.Log("✅ GameOverManager trovato. Chiamo PlayerDied()...");
            gm.PlayerDied(); 
        }
        else
        {
            Debug.LogError("GameOverManager è null! Il panel di Game Over non apparirà.");
            
        }

    }

    void LoadMainMenuFallback()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseMaxHealth(int amount = 1)
    {
        maxHealth += amount;
        currentHealth += amount;
        Debug.Log($"❤️ Vita aumentata a {maxHealth} (attuale: {currentHealth})");
    }

    public void SaveCurrentHealth()
    {
        PlayerPrefs.SetInt("SavedCurrentHealth", currentHealth);
        PlayerPrefs.SetInt("SavedMaxHealth", maxHealth);
        PlayerPrefs.Save();
        Debug.Log($"💾 Vita salvata: {currentHealth}/{maxHealth}");
    }

   public int CurrentHealth => currentHealth;
}