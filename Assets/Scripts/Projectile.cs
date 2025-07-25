using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;

    void Start()
    {
        FindClosestEnemy();
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = enemy.transform;
            }
        }

        target = closest;
    }

   void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger con: " + collision.name);  // Verifica cosa tocchi

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Colpito un nemico!");

            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collisione rigida con: " + collision.gameObject.name);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}


