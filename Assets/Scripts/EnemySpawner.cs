using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 5;

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
        Vector2 spawnPos = GetRandomOffscreenPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        enemy.GetComponent<Enemy>().spawner = this;

        currentEnemies++;
    }

    public void EnemyDied()
    {
        currentEnemies--;
    }

    Vector2 GetRandomOffscreenPosition()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        int side = Random.Range(0, 4); // 0 = top, 1 = bottom, 2 = left, 3 = right
        float x = 0, y = 0;

        switch (side)
        {
            case 0: 
                x = Random.Range(-camWidth / 2, camWidth / 2);
                y = cam.orthographicSize + 1;
                break;
            case 1: 
                x = Random.Range(-camWidth / 2, camWidth / 2);
                y = -cam.orthographicSize - 1;
                break;
            case 2: 
                x = -camWidth / 2 - 1;
                y = Random.Range(-camHeight / 2, camHeight / 2);
                break;
            case 3: 
                x = camWidth / 2 + 1;
                y = Random.Range(-camHeight / 2, camHeight / 2);
                break;
        }

        return new Vector2(x, y);
    }
}
