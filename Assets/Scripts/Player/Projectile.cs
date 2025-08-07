using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    private Vector2 targetDirection;
    private Transform target;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }

            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
