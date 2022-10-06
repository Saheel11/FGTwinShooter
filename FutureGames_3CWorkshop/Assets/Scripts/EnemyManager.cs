using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawnTimer;
    public float enemySpawnTimeCooldown = 5; 
    public int maxAmountOfEnemies;
    public int amountOfEnemiesInLevel;


    [SerializeField] Transform[] possibleSpawnPositions;
    bool[] usedSpawnPositions;
    
    public PlayerStats playerStats;



    int checkedSpawnPositions;
    

    private void FixedUpdate()
    {
        enemySpawnTimer += Time.deltaTime; //Starting timer for enemy to spawn
        if (enemySpawnTimer >= enemySpawnTimeCooldown)
        {
            RandomSpawn();
            enemySpawnTimer = 0; //resets timer to 0 every time an enemy spawn
        }
        
        // Decrease time between enemies spawning & limit amount of enemies spawning depending on the meter 
        if (playerStats.meter > 80) 
        {
            enemySpawnTimeCooldown = 0.5f;
            maxAmountOfEnemies = 50;
        }
        else if (playerStats.meter > 60) 
        {
            enemySpawnTimeCooldown = 1;
            maxAmountOfEnemies = 40;
        }
        else if (playerStats.meter > 40) 
        {
            enemySpawnTimeCooldown = 1.25f;
            maxAmountOfEnemies = 30;
        }
        else if (playerStats.meter > 20) 
        {
            enemySpawnTimeCooldown = 1.5f;
            maxAmountOfEnemies = 20;
        }
        else if (playerStats.meter < 20)
        {
            enemySpawnTimeCooldown = 2;
            maxAmountOfEnemies = 10;
        }
    }
    
    public void RandomSpawn()
    {
        if (amountOfEnemiesInLevel <= maxAmountOfEnemies)
        {
            int spawnIndex = Random.Range(0, possibleSpawnPositions.Length);
            
            if (spawnIndex == checkedSpawnPositions)
            {
                spawnIndex++;
                spawnIndex %= possibleSpawnPositions.Length;
              
            }    
        
            if (spawnIndex > possibleSpawnPositions.Length)
            {
                spawnIndex = 0;
            }

            GameObject newEnemy = Instantiate(enemyPrefab, possibleSpawnPositions[spawnIndex].position, possibleSpawnPositions[spawnIndex].rotation);
            checkedSpawnPositions = spawnIndex;
            amountOfEnemiesInLevel++;
           

        }

    }
}
