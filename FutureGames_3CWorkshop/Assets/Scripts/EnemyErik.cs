using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyErik : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public LayerMask playerMask;

    // vad ska enemyn kunna göra? 
    //rör sig
    //rotera
    //skjuta
    //den är baserad på enemy prefab??

    //kanske ska göras som en navmeshagent??

    //hur gör man om det inte är navmesh??
    //move o rotate borde troligtvis ligga i update så att det sker konstant (ingen player input behövs)
    //sen införa några if-statements. 
    //tex skapa en konstant osynling raycast som om den träffar spelaren gör enemyn något.
    //låter bra!



    public Vector3 enemyTargetPosition;
    public float enemySpeed;
    public float enemyY;
    public float timer;
    public float maxTimer;


    void Start()
    {
        enemyTargetPosition = new Vector3(Random.Range(1f, 2f), enemyY, Random.Range(1f, 2f));
        enemyAgent = GetComponent<NavMeshAgent>();

        timer = maxTimer;
    }


  void Update()
  {
        EnemyMove();                   //anävnd movetowards.

        EnemyRotate();

        CheckForPlayer();

        enemyY = transform.position.y;

        timer -= Time.deltaTime;

    }

    public void AttackPlayer()
    {
        //shoot projectile
    }

    void EnemyMove()                   //anävnd movetowards.
    {
        // Move our position a step closer to the target.
        var step = enemySpeed * Time.deltaTime; // calculate distance to move
        //transform.position = Vector3.MoveTowards(transform.position, enemyTargetPosition, step);
        
        enemyAgent.SetDestination(enemyTargetPosition);

     

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, enemyTargetPosition) < 0.001f)
        {
            // Swap the position of the cylinder.
            enemyTargetPosition *= -1.0f;
        }

        if (transform.position == new Vector3(enemyTargetPosition.x, enemyY, enemyTargetPosition.z) || timer <= 0)
        {

            enemyTargetPosition = new Vector3(Random.Range(1f, 10f), enemyY, Random.Range(1f, 10f));
            Debug.Log("jejeaje");
            timer = maxTimer;
        }




    }



    void EnemyRotate()
    {
        Vector3 name = new Vector3(0f, 0.2f, 0f);
        transform.Rotate(name);
    }

    void CheckForPlayer()
    {
        RaycastHit result;
        bool seePlayer = Physics.Raycast(transform.position, transform.forward, out result, Mathf.Infinity, playerMask);

        if (seePlayer)
        {
            Debug.Log("hej" + result.collider.name);
            AttackPlayer();
        }
    }

    
}
