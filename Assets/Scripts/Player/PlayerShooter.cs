using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public bool hasDoubleShot = false;
    private float nextFireTime = 0f;
    private Vector2 lastDirection = Vector2.down;

    private Animator anim;
    private string[] gameScenes = { "Level1", "Level2", "Level3" };
    private bool IsInGameScene()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return System.Array.Exists(gameScenes, scene => scene == currentScene);
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        int hasDouble = PlayerPrefs.GetInt("HasDoubleShot", 0);
        if (hasDouble == 1)
        {
            hasDoubleShot = true;
            Debug.Log("⚡ Doppio tiro caricato!");
        }
        else
        {
            Debug.Log("🔸 Doppio tiro disabilitato");
        }
    }

    void Update()
    {
        if (!IsInGameScene())
            return;
    
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

    public void EnableDoubleShot()
    {
        hasDoubleShot = true;
    }

    void Shoot()
    {
        if (!IsInGameScene()) return;

        Vector2 shootDirection = FindDirectionToClosestEnemy();


        anim.SetFloat("LastX", shootDirection.x);
        anim.SetFloat("LastY", shootDirection.y);
        anim.SetTrigger("IsAttacking");


        StartCoroutine(DelayedShoot(shootDirection));
        
        if (hasDoubleShot)
    {
        Vector2 offset = new Vector2(-shootDirection.y, shootDirection.x).normalized;
        
        StartCoroutine(DelayedShoot(shootDirection + offset * 0.3f, 0.1f));
        StartCoroutine(DelayedShoot(shootDirection - offset * 0.3f, 0.1f));
    }
    }

    Vector2 Perpendicular(Vector2 v)
    {
        return new Vector2(-v.y, v.x).normalized;
    }

    Vector2 FindDirectionToClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        List<Transform> allTargets = new List<Transform>();

        foreach (GameObject enemy in enemies)
        {
            allTargets.Add(enemy.transform);
        }

        foreach (GameObject boss in bosses)
        {
            allTargets.Add(boss.transform);
        }

        if (allTargets.Count == 0)
        {
            return lastDirection == Vector2.zero ? Vector2.up : lastDirection;
        }

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform target in allTargets)
        {
            float dist = Vector2.Distance(firePoint.position, target.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = target;
            }
        }

        return (closest.position - firePoint.position).normalized;
    }


    IEnumerator DelayedShoot(Vector2 direction, float extraDelay = 0f)
    {
        yield return new WaitForSeconds(0.3f + extraDelay);

        if (!IsInGameScene()) yield break;

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
