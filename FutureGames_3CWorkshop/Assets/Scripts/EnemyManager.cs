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

    /*[Header("Random Spawn Positions")]
    public float minXValue = 1;
    public float maxXValue = 50;
    public float minZValue = 1;
    public float maxZValue = 50;
    */
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
            //L�G IN H�R ATT RESETTA ALLA SPAWNPUNKTER
        }
    }

    /* public void SpawnNewEnemies() // instantiates enemies randomly on the x and z axis
     {
         GameObject newEnemy = Instantiate(enemyPrefab);
         //newEnemy.transform.position = new Vector3(Random.Range(minXValue, maxXValue), 0, Random.Range(minZValue, maxZValue));

     }

     public Vector3 GetSpawnPosition(int position)
     {
         if (position < spawnpositions.Length)
         {
             return spawnpositions[position];
         }
         return null;
     }
     */

    public void RandomSpawn()
    {
       // int checkedSpawnPositions = 0;
        int spawnIndex = Random.Range(0, possibleSpawnPositions.Length);
      
        if (spawnIndex == checkedSpawnPositions)
        {
           
            spawnIndex++;
            if (spawnIndex > possibleSpawnPositions.Length)
            {
                spawnIndex = 0;
            }
            
        }    
       /* int checkedSpawnPositions = 0;
        while (usedSpawnPositions[spawnIndex])
        {
            spawnIndex = (spawnIndex + 1) % possibleSpawnPositions.Length;
            checkedSpawnPositions++;
            if (checkedSpawnPositions >= possibleSpawnPositions.Length)
            {
                checkedSpawnPositions = 0;               
            }
            
        }*/
        GameObject newEnemy = Instantiate(enemyPrefab, possibleSpawnPositions[spawnIndex]);
        checkedSpawnPositions = spawnIndex;
       
        // newEnemy.transform.position = possibleSpawnPositions[spawnIndex];
    }
}
