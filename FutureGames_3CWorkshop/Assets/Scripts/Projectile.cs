using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    public String whatIHit;

    public PlayerStats playerStats;

    public NavMeshAgent enemyAgent;

    public EnemyErik enemyScript;

    public AudioSource audioSource;

    public AudioClip playerHit;
    public AudioClip enemieHit;

    public bool iHaveNotHit;


    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        iHaveNotHit = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(whatIHit) && iHaveNotHit) //Added if statement for it to destroy objects with name Destroyable in hierarchy
        {
            if (other.gameObject.name == "Player")
            {
                audioSource.clip = playerHit;
                audioSource.Play();

                playerStats = other.gameObject.GetComponent<PlayerStats>();
                playerStats.DecreaseMeter();
                Debug.Log("Projectile destroys itself");
                Destroy(this.gameObject, 1f);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                audioSource.clip = enemieHit;
                audioSource.Play();
                enemyAgent = other.gameObject.GetComponent<NavMeshAgent>();
                enemyScript = other.gameObject.GetComponent<EnemyErik>();
                enemyAgent.speed = 0;
                enemyScript.canIShoot = false;
                Debug.Log("Projectile destroys itself");
                
                Destroy(this.gameObject, 1f);
            }

          ;
            GetComponent<MeshRenderer>().enabled = false;
           
            iHaveNotHit = false;
        }
        

    }
}
