using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private EnemyAnimation anim;
    public EnemySpawner spawner;

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
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        anim.PlayRun(true);
    }

    public void Die()
    {
        anim.PlayDeath();
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            anim.PlayHurt();
            spawner?.EnemyDied();
            Die();
        }
    }
}
