using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public GameObject playerPrefab;

    private void Start()
    {

        SpawnPlayer();
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
}
