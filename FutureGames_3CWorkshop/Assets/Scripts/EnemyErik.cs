using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyErik : MonoBehaviour
{
    public Transform playerTarget;
    public NavMeshAgent enemyAgent;
    public LayerMask playerMask;

    [Header("Enemy moving")]
    public Vector3 enemyTargetPosition;
    public float enemySpeed;
    public float enemyY;
    public float timerMove;
    public float maxTimerMove;

    [Header("Enemy shooting")]
    public Transform firePoint;
    public GameObject projectile;
    public float projectileSpeed;

    public float timerShoot;
    public float maxTimerShoot;
    public bool canIShoot;
    
    [Header("Enemy movement: Random positions")] 
    public float minXValue = 1;
    public float maxXValue = 50;
    public float minZValue = 1;
    public float maxZValue = 50;



    void Start()
    {
        enemyTargetPosition = new Vector3(Random.Range(minXValue, maxXValue), enemyY, 
            Random.Range(minZValue, maxZValue));
        enemyAgent = GetComponent<NavMeshAgent>();

        timerShoot = maxTimerShoot;
        timerMove = maxTimerMove;
        canIShoot = true;
    }

  void Update()
  {
        EnemyMove();                   
        CheckForPlayer();
        enemyY = transform.position.y;
        timerMove -= Time.deltaTime;
        timerShoot += Time.deltaTime;
  }

    public void AttackPlayer()
    {       
        transform.LookAt(playerTarget);
        if (timerShoot > 2 && canIShoot)
        {
            OnFireEnemy();
            timerShoot = 0;
            
        }
    }

    void EnemyMove()         
    {
        // Move our position a step closer to the target.
        var step = enemySpeed * Time.deltaTime; // calculate distance to move
        enemyAgent.SetDestination(enemyTargetPosition);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, enemyTargetPosition) < 0.001f)
        {
            // Swap the position of the cylinder.
            enemyTargetPosition *= -1.0f;
        }

        if (transform.position == new Vector3(enemyTargetPosition.x, enemyY, enemyTargetPosition.z) || timerMove <= 0)
        {
            enemyTargetPosition = new Vector3(Random.Range(minXValue, maxXValue), enemyY, Random.Range(minZValue, maxZValue));
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
