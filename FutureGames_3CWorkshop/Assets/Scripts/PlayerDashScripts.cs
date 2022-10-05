using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerDashScripts : MonoBehaviour
{
    CharacterController characterController;
    public PlayerStats playerStats;
    public Shooting shooting;
    int go;

    public float dashFloat = 30;
    public float maxTimer;
    public float dashTimer;

    public LayerMask mask;
    public Collider[] colliders;

    [HideInInspector] public bool imDashing;
    [HideInInspector] public Vector2 rightStickPosition;
    
    [HideInInspector] public float resetDashCooldown = 0f;

    public RaycastHit hit;

    public AudioSource audioSource;
    public AudioClip clipDash;
    public AudioClip clipDeath;

    public GameObject dashParticle;
    [HideInInspector] public GameObject dashParticlePosition;
    public GameObject hitEnemyParticle;
    [HideInInspector] public GameObject hitEnemyParticleClone;
    public Transform transformer;
    public Transform playerTransform;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        shooting = GetComponent<Shooting>();
        audioSource = GetComponent<AudioSource>();
        transformer = Instantiate(playerTransform, transform);
    }


    void Update()
    {

        transformer.position = transform.position;
        Vector3 testDirection = new Vector3(rightStickPosition.x, 0, rightStickPosition.y);
        
        colliders = Physics.OverlapSphere(transform.position, 1, mask);
  
        if (imDashing)
        {
          characterController.Move(transformer.transform.forward * dashFloat * Time.deltaTime);
          
          //dashParticle.transform.position = transform.position;
          
          dashTimer -=Time.deltaTime; 
            
            if(dashTimer < 0)
            {
                Destroy(dashParticlePosition);
                imDashing = false;
            }
            foreach (var hitCollerer in colliders)
            {
                Debug.Log("iHit" + hitCollerer.gameObject.name);
                playerStats.dashCooldown = resetDashCooldown;
                playerStats.IncreaseMeter();
                shooting.IncreaseAmmo(1); // switch this to check how many player have killed
                Debug.Log(shooting.projectileAmmo);
                hitEnemyParticleClone = Instantiate(hitEnemyParticle, hitCollerer.transform);
                

                //hitCollerer.GetComponent<AudioSource>().clip = clipDeath;
                if (hitCollerer.GetComponent<AudioSource>() != null)
                {
                    hitCollerer.GetComponent<AudioSource>().Play();

                    if(hitCollerer.GetComponent<EnemyErik>() != null)
                    {
                        hitCollerer.GetComponent<EnemyErik>().canIShoot = false;
                        hitCollerer.GetComponent<NavMeshAgent>().speed = 0;
                        hitCollerer.GetComponent<BoxCollider>().enabled = false;
                    }

                    Destroy(hitCollerer.gameObject, 1.2f);
                }
                else
                {
                    Destroy(hitCollerer.gameObject);

                }


               // Destroy(hitCollerer.gameObject, 1.2f);
            }
        }
    }
    public void OnDash()
    {
        if (playerStats.canDash == true)
        {
            imDashing = true;
            dashTimer = maxTimer;
            playerStats.StartDashCooldown();
            audioSource.clip = clipDash;
            audioSource.Play();
            dashParticlePosition = Instantiate(dashParticle, transform);
        }   

    }


    
    public void OnMove(InputValue lookValue)
    {
        rightStickPosition = lookValue.Get<Vector2>();

        if (rightStickPosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(rightStickPosition.x, rightStickPosition.y) * Mathf.Rad2Deg;
            transformer.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
