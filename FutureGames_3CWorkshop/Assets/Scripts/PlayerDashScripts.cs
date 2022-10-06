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

    public float dashFloat = 30;
    public float maxTimer;
    public float dashTimer;

    public LayerMask mask;
    public Collider[] colliders;

    [HideInInspector] public bool imDashing;
    [HideInInspector] public Vector2 leftStickPosition;
    
    [HideInInspector] public float resetDashCooldown = 0f;

    public RaycastHit hit;

    public AudioSource audioSource;
    public AudioClip clipDash;
    public AudioClip clipDeath;

    public GameObject dashParticle;
    [HideInInspector] public GameObject dashParticlePosition;
    public GameObject hitEnemyParticle;
    [HideInInspector] public GameObject hitEnemyParticleClone;
    [HideInInspector] public Transform transformer;
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
        Vector3 testDirection = new Vector3(leftStickPosition.x, 0, leftStickPosition.y);
        
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
            foreach (var hitCollider in colliders)
            {
                playerStats.dashCooldown = resetDashCooldown;
                playerStats.IncreaseMeter();
                shooting.IncreaseAmmo(1); 
                hitEnemyParticleClone = Instantiate(hitEnemyParticle, hitCollider.transform);
               

                if (hitCollider.GetComponent<AudioSource>() != null)
                {
                    hitCollider.GetComponent<AudioSource>().Play();
                    kills += 1;
                    if (hitCollider.GetComponent<EnemyErik>() != null)
                    {
                        hitCollider.GetComponent<EnemyErik>().canIShoot = false;
                        hitCollider.GetComponent<NavMeshAgent>().speed = 0;
                        hitCollider.GetComponent<BoxCollider>().enabled = false;
                        enemyManager.RandomSpawn();
                        enemyManager.amountOfEnemiesInLevel--;
                    }
                    Destroy(hitCollider.gameObject, 1.2f);
                }
                else
                {
                    Destroy(hitCollider.gameObject);

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
        leftStickPosition = lookValue.Get<Vector2>();

        if (leftStickPosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(leftStickPosition.x, leftStickPosition.y) * Mathf.Rad2Deg;
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
