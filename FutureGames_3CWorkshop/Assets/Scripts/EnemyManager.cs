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
    public float enemySpawnTimeCooldown = 5; 

    [SerializeField] Transform[] possibleSpawnPositions;
    bool[] usedSpawnPositions;
    public PlayerStats playerStats;
    private int[] enemyAmount;


    int checkedSpawnPositions;
    
    private void Awake()
    {
        //usedSpawnPositions = new bool[possibleSpawnPositions.Length];
    }
    private void Start()
    {
       //possibleSpawnPositions = new Transform[10];
    }
    private void FixedUpdate()
    {
        enemySpawnTimer += Time.deltaTime; //Starting timer for enemy to spawn
        if (enemySpawnTimer >= enemySpawnTimeCooldown)
        {
            Debug.Log("Spawning 1 enemy");
            RandomSpawn();
            enemySpawnTimer = 0; //resets timer to 0 every time an enemy spawn
        }
        
        // Decrease time between enemies spawning depending on the meter
        if (playerStats.meter >= 80) 
        {
            enemySpawnTimeCooldown = 0.5f;
        }
        else if (playerStats.meter >= 60) 
        {
            enemySpawnTimeCooldown = 1;
        }
        else if (playerStats.meter >= 40) 
        {
            enemySpawnTimeCooldown = 1.25f;
        }
        else if (playerStats.meter >= 20) 
        {
            enemySpawnTimeCooldown = 1.5f;
        }
        else if (playerStats.meter < 20)
        {
            enemySpawnTimeCooldown = 2;
        }
    }
    
    public void RandomSpawn()
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

    }
}
