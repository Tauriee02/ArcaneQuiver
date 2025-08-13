using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    private Vector2 targetDirection;
    private Transform target;
    public bool isEnemyProjectile = false;

    void Start()
    {

        Destroy(gameObject, 5f);
        if (target != null)
        {
            targetDirection = (target.position - transform.position).normalized;
        }
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }



    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemyProjectile)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth player = other.GetComponent<PlayerHealth>();
                if (player != null)
                {
                    player.TakeDamage();
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Die();
                }
                Destroy(gameObject);
            }
            else if (other.CompareTag("Boss"))
            {
                Boss boss = other.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.TakeDamage();
                }
                Destroy(gameObject);
            }
        }
}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
