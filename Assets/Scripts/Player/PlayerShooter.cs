using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
    private Vector2 lastDirection = Vector2.down;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveDir.magnitude > 0.1f)
        {
            lastDirection = moveDir;
        }

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        Vector2 shootDirection = FindDirectionToClosestEnemy();

       
        anim.SetFloat("LastX", shootDirection.x);
        anim.SetFloat("LastY", shootDirection.y);
        anim.SetTrigger("IsAttacking");

       
        StartCoroutine(DelayedShoot(shootDirection));
    }

    Vector2 FindDirectionToClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(firePoint.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = enemy.transform;
            }
        }

        if (closest != null)
        {
            return (closest.position - firePoint.position).normalized;
        }

        return Vector2.right; 
    }


    IEnumerator DelayedShoot(Vector2 direction)
    {
        
        yield return new WaitForSeconds(0.3f);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.SetDirection(direction);
        }

        StartCoroutine(ResetAttackTrigger());
    }


    IEnumerator ResetAttackTrigger()
    {
        yield return new WaitForSeconds(0.2f); 
        anim.ResetTrigger("IsAttacking");
    }


    
}
