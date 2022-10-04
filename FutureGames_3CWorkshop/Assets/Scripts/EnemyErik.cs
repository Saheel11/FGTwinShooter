using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyErik : MonoBehaviour
{
    public Transform playerTarget;
    public NavMeshAgent enemyAgent;
    public LayerMask playerMask;
   
    public Vector3 enemyTargetPosition;
    public float enemySpeed;
    public float enemyY;
    public float timerMove;
    public float maxTimerMove;

    //ShootStuff
    public Transform firePoint;
    public GameObject projectile;
    public float projectileSpeed;

    public float timerShoot;
    public float maxTimerShoot;



    void Start()
    {
        enemyTargetPosition = new Vector3(Random.Range(1f, 20f), enemyY, Random.Range(1f, 20f));
        enemyAgent = GetComponent<NavMeshAgent>();

        timerShoot = maxTimerShoot;
        timerMove = maxTimerMove;
    }

  void Update()
  {
        EnemyMove();                   

        //EnemyRotate();

        CheckForPlayer();

        enemyY = transform.position.y;

        timerMove -= Time.deltaTime;
        timerShoot += Time.deltaTime;
  }

    public void AttackPlayer()
    {
        //here we add that the enemy should stop, look towards the player and then shoot. after a while it should move again. 
        //shoot projectile
        transform.LookAt(playerTarget);
        if (timerShoot > 2)
        {
            OnFireEnemy();
            timerShoot = 0;
            
        }

    }

    void EnemyMove()         
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

        if (transform.position == new Vector3(enemyTargetPosition.x, enemyY, enemyTargetPosition.z) || timerMove <= 0)
        {
            enemyTargetPosition = new Vector3(Random.Range(1f, 20f), enemyY, Random.Range(1f, 20f));
            Debug.Log("jejeaje");
            timerMove = maxTimerMove;
        }
    }
   
    void CheckForPlayer()
    {
        RaycastHit result;
        bool seePlayer = Physics.Raycast(transform.position, transform.forward, out result, Mathf.Infinity, playerMask);

        Debug.DrawRay(transform.position, transform.forward * 50f, Color.red, 0.01f);
        if (seePlayer)
        {
            Debug.Log("hej" + result.collider.name);
            AttackPlayer();
        }
    }

    public void OnFireEnemy()
    {        
        {
            GameObject enemyProjectile = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
            enemyProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
            enemyProjectile.GetComponent<Projectile>().whatIHit = "OnlyPlayer";          
            Destroy(enemyProjectile, 2f);
        }
    }

    /*void EnemyRotate()        NOT USED RIGHT NOW
    {
        Vector3 name = new Vector3(0f, 0.2f, 0f);
        transform.Rotate(name);
    }
    */
}
