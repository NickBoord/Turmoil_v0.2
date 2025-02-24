using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    private static GameObject currentEnemy;
    public float positionX = 10;
    public float positionY = 10;
    
    
    void Start()
    {
        if (currentEnemy == null)
        {
            SpawnEnemy();
        }
    }

    
    void SpawnEnemy ()
    {
        Vector3 spawnPosition = new Vector3(positionX, positionY, 0); // Adjust as needed
        currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
