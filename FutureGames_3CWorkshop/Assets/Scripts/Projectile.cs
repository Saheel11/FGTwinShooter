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
    public AudioClip litHit;

    public bool iHaveNotHit;
    
    public GameObject stunParticle;
    [HideInInspector] public GameObject stunParticleClone;
    
    public GameObject playerGotHitParticle;
    [HideInInspector] public GameObject playerGotHitParticleClone;


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
                if(other.GetComponent<PlayerDashScripts>().imDashing == true)
                {

                    audioSource.clip = litHit;
                    audioSource.Play();

                }
                else
                {

                    audioSource.clip = playerHit;
                    audioSource.Play();
                    playerStats = other.gameObject.GetComponent<PlayerStats>();
                    playerStats.DecreaseMeter();
                    playerGotHitParticleClone = Instantiate(playerGotHitParticle, other.transform);
                    Destroy(playerGotHitParticleClone, 1f);

                }

                //audioSource.clip = playerHit;
                //audioSource.Play();
                //playerStats = other.gameObject.GetComponent<PlayerStats>();
                //playerStats.DecreaseMeter();
                //playerGotHitParticleClone = Instantiate(playerGotHitParticle, other.transform);
                //Destroy(playerGotHitParticleClone, 1f);
                Debug.Log("Projectile destroys itself");
                iHaveNotHit = false;
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
                stunParticleClone = Instantiate(stunParticle, other.transform);
                Debug.Log("Projectile destroys itself");
                iHaveNotHit = false;
                Destroy(this.gameObject, 1f);
            }

          
            GetComponent<MeshRenderer>().enabled = false;
           

        }
        

    }
}
