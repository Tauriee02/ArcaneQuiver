using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
     public Transform[] enemySpawnPoints;
     private int nextSpawnIndex = 0;
    public float spawnInterval = 1f;
    public int maxEnemies = 10;
    public int currentEnemies = 0; 
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval  && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if(enemySpawnPoints.Length == 0)
        {
            Debug.LogWarning("Nessun punto spawn nemici assegnato!");
            return;
        }

        Vector2 spawnPos = enemySpawnPoints[nextSpawnIndex].position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        enemy.GetComponent<Enemy>().spawner = this;
        currentEnemies++;

        nextSpawnIndex = (nextSpawnIndex + 1) % enemySpawnPoints.Length;
    }

    public void EnemyDied()
    {
        currentEnemies--;
    }

}
