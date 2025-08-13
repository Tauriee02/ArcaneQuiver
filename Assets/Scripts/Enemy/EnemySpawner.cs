using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    private int nextSpawnIndex = 0;

    [Header("Configurazione Base")]
    public float spawnInterval = 0.5f;
    public int maxEnemies = 10;
    private float timer = 0f;
    public int currentEnemies = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timer = 0f;
        currentEnemies = 0;

        switch (scene.name)
        {
            case "Level1":
                spawnInterval = 1.5f;
                maxEnemies = 5;
                break;

            case "Level2":
                spawnInterval = 1f;   
                maxEnemies = 8;        
                break;

            default:
                spawnInterval = 1f;
                maxEnemies = 10;
                break;
        }

        Debug.Log($"🔄 EnemySpawner aggiornato per {scene.name}: " +
                  $"intervallo={spawnInterval:F1}s, maxNemici={maxEnemies}");
    }

    void Update()
    {

        if (Time.timeScale == 0f) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemySpawnPoints.Length == 0)
        {
            Debug.LogWarning("Nessun punto spawn nemici assegnato!");
            return;
        }

        Vector2 spawnPos = enemySpawnPoints[nextSpawnIndex].position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.spawner = this;
        }

        currentEnemies++;
        nextSpawnIndex = (nextSpawnIndex + 1) % enemySpawnPoints.Length;
    }

    public void EnemyDied()
    {
        currentEnemies--;
    }
}