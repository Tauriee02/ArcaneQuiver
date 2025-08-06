using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vita")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Riferimenti")]
    public GameObject gameOverManager; 
    private GameOverManager gm;

    [Header("Feedback Visivo")]
    public float invincibilityDuration = 1.5f; 
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        
        if (gameOverManager == null)
        {
            GameObject goManager = GameObject.Find("GameOverManager");
            if (goManager != null)
            {
                gm = goManager.GetComponent<GameOverManager>();
            }
        }
        else
        {
            gm = gameOverManager.GetComponent<GameOverManager>();
        }
    }


    public void TakeDamage()
    {
        if (isInvincible) return;

        currentHealth--;

        
        isInvincible = true;
        StartCoroutine(FlashPlayer());
        Invoke(nameof(ResetInvincibility), invincibilityDuration);

        
        animator.SetTrigger("Hurt");


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
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayPlayerDeath();

        
        enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShooter>().enabled = false;

        
        if (gm != null)
        {
            gm.PlayerDied();
        }
        else
        {
            Debug.LogError("GameOverManager non trovato!");
        }

        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }


    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
}