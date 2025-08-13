using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private EnemyAnimation anim;
    public EnemySpawner spawner;
    private bool isDead = false;


    void Start()
    {
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player non trovato! Assicurati che abbia il tag 'Player'");
        }

        anim = GetComponent<EnemyAnimation>();
    }

   void Update()
    {
        if (player == null || isDead) return; 

        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        anim.PlayRun(true);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        var rb = GetComponent<Rigidbody2D>();
        if (rb) rb.simulated = false;

        anim.PlayDeath();
        spawner?.EnemyDied();
        LevelManager.Instance.EnemyKilled();
        Destroy(gameObject, 1f);
        Debug.Log($"Nemico ucciso! Totale uccisi: {LevelManager.Instance.enemiesKilled + 1}");
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            
            Die();
        }
        else if (collision.CompareTag("Player"))
        {
            
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
        }
    }
}
