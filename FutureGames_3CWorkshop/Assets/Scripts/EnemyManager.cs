using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawnTimer;
    public int amountOfEnemiesSpawned;
    public int enemySpawnTimeCooldown = 5;

    private void FixedUpdate()
    {
        enemySpawnTimer += Time.deltaTime; //Starting timer for enemy to spawn
        if (enemySpawnTimer >= enemySpawnTimeCooldown)
        {
            Debug.Log("Spawning 1 enemy");
            SpawnNewEnemies();
            enemySpawnTimer = 0; //resets timer to 0 every time an enemy spawn
            amountOfEnemiesSpawned++; //increase an int to check how many enemies have spawned
        }
        
        // For testing purposes, if we want more enemies to spawn
        if (amountOfEnemiesSpawned == 5) //if five enemies in total have spawned, decrease spawnTime to 3
        {
            enemySpawnTimeCooldown = 3;
        }
        
        if (amountOfEnemiesSpawned == 10) //if ten enemies in total have spawned, decrease spawnTime to 1
        {
            enemySpawnTimeCooldown = 1;
        }
    }

    public void SpawnNewEnemies() // instantiates enemies randomly on the x and z axis
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = new Vector3(Random.Range(1f, 10f), 0, Random.Range(1f, 10f));
    }
}
