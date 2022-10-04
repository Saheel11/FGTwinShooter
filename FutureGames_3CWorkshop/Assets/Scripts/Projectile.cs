using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    public String whatIHit;

    public PlayerStats playerStats;

    public NavMeshAgent enemyAgent;

    public EnemyErik enemyScript;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(whatIHit)) //Added if statement for it to destroy objects with name Destroyable in hierarchy
        {
            if (other.gameObject.name == "Player")
            {
                playerStats = other.gameObject.GetComponent<PlayerStats>();
                playerStats.DecreaseMeter();
                Debug.Log("Projectile destroys itself");
                Destroy(this.gameObject);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyAgent = other.gameObject.GetComponent<NavMeshAgent>();
                enemyScript = other.gameObject.GetComponent<EnemyErik>();
                enemyAgent.speed = 0;
                enemyScript.canIShoot = false;
                Debug.Log("Projectile destroys itself");
                Destroy(this.gameObject);
            }

        }
        

    }
}
