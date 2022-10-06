using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

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
    public EnemyManager enemyManager;

    public GameObject extraDashParticle;
    [HideInInspector] public GameObject extraDashParticleClone;

    public TextMeshProUGUI dashPlusText;

    public bool extraDash;
    public bool extraDashRemove;
    public int kills;
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

          dashTimer -=Time.deltaTime;

        

            if (dashTimer < 0)
            {
                Destroy(dashParticlePosition);
                imDashing = false;
                DashPowerUp(kills);
            }
            foreach (var hitCollerer in colliders)
            {
                playerStats.dashCooldown = resetDashCooldown;
                playerStats.IncreaseMeter();
                shooting.IncreaseAmmo(1); 
                hitEnemyParticleClone = Instantiate(hitEnemyParticle, hitCollerer.transform);
               

                if (hitCollerer.GetComponent<AudioSource>() != null)
                {
                    hitCollerer.GetComponent<AudioSource>().Play();
                    kills += 1;
                    if (hitCollerer.GetComponent<EnemyErik>() != null)
                    {
                        hitCollerer.GetComponent<EnemyErik>().canIShoot = false;
                        hitCollerer.GetComponent<NavMeshAgent>().speed = 0;
                        hitCollerer.GetComponent<BoxCollider>().enabled = false;
                        enemyManager.RandomSpawn();
                        enemyManager.amountOfEnemiesInLevel--;
                    }
                    Destroy(hitCollerer.gameObject, 1.2f);
                }
                else
                {
                    Destroy(hitCollerer.gameObject);

                }
            }
        }
    }
    public void OnDash()
    {
        if (playerStats.canDash == true)
        {
            imDashing = true;
            dashTimer = maxTimer;
            audioSource.clip = clipDash;
            audioSource.Play();
            dashParticlePosition = Instantiate(dashParticle, transform);
            Destroy(extraDashParticleClone);
            dashPlusText.gameObject.SetActive(false);
            if (extraDash == false)
            {
                playerStats.StartDashCooldown();
            }
            else
            {

                extraDash = false;

            }
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

    public void DashPowerUp(int howManyKílled)
    {
        if (howManyKílled >= 2)
        {
            extraDash = true;
            extraDashParticleClone = Instantiate(extraDashParticle, transform);
            dashPlusText.gameObject.SetActive(true);

        }

        kills = 0;


    }
}
