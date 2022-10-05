using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; //DET H�R �R GAMEOBJECTET VI SPAWNAR
    public float enemySpawnTimer; //DEN H�R TICKAR UPP HELA TIDEN OCH J�MF�RS MED SPWANCOOLDOWN
    public int amountOfEnemiesSpawned; //DEN H�R L�GGER TILL EN VARJE G�NG EN ENEMY SPAWNAR. VI ANV�NDER DEN F�R ATT G�RA ETT IF STATEMENT SOM �NDRAR CD P� SPWAN
    public int enemySpawnTimeCooldown = 5; //V�RAN URSPRINGLIGA CD P� SPAWN. �NDRAS UNDER SPELETS G�NG

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
            enemySpawnTimeCooldown = 1;
        }
        else if (playerStats.meter >= 60) 
        {
            enemySpawnTimeCooldown = 2;
        }
        else if (playerStats.meter >= 40) 
        {
            enemySpawnTimeCooldown = 3;
        }
        else if (playerStats.meter >= 20) 
        {
            enemySpawnTimeCooldown = 4;
        }
        else if (playerStats.meter < 20)
        {
            enemySpawnTimeCooldown = 5;
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
