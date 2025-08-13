using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Vita")]
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Sparo")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    public int bulletsPerShot = 12;     // Quanti proiettili per raffica
    public float spiralStep = 10f;      // Rotazione della spirale per ogni raffica
    public float bulletSpeed = 5f;

    [Header("Riferimenti")]
    public Animator anim;

    private bool isDead = false;
    private float currentSpiralAngle = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        InvokeRepeating(nameof(Fire), 1f, fireRate);
    }

    void Fire()
    {
        if (isDead) return;

        anim.SetTrigger("Attack");

        float angleStep = 360f / bulletsPerShot;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            float angle = currentSpiralAngle + (i * angleStep);
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(fireballPrefab, firePoint.position, rot);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = rot * Vector2.up * bulletSpeed;
            }
        }

        currentSpiralAngle += spiralStep;
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentHealth--;
        Debug.Log($"🔥 Boss colpito! Vita: {currentHealth}/{maxHealth}");
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        LevelManager.Instance?.EnemyKilled();

        CancelInvoke(nameof(Fire));
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        anim.SetTrigger("Die");

        Invoke(nameof(CompleteLevel), 2f);
    }

    void CompleteLevel()
    {
        LevelManager.Instance?.ShowNextLevelUI();
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
