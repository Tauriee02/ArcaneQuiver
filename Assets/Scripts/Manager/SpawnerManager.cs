using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [Header("Spawn Points")]
    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Player prefab o spawn point mancante!");
        }
    }

    void SpawnEnemies()
    {
        if (enemyPrefab != null && enemySpawnPoints.Length > 0)
        {
            foreach (Transform spawnPoint in enemySpawnPoints)
            {
                if (spawnPoint != null)
                    Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }   
        }
        else
        {
            Debug.LogWarning("Enemy prefab o spawn points mancanti!");
        }
    }
}
