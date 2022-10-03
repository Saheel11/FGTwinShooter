using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
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


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        shooting = GetComponent<Shooting>();
    }


    void Update()
    {
        Vector3 testDirection = new Vector3(rightStickPosition.x, 0, rightStickPosition.y);
        
        colliders = Physics.OverlapSphere(transform.position, 1, mask);
  
        if (imDashing)
        {
          characterController.Move(transform.forward * dashFloat * Time.deltaTime);
          dashTimer -=Time.deltaTime; 
            
            if(dashTimer < 0)
            {
                imDashing = false;
            }
            foreach (var hitCollerer in colliders)
            {
                Debug.Log("iHit" + hitCollerer.gameObject.name);
                playerStats.dashCooldown = resetDashCooldown;
                playerStats.IncreaseMeter();
                shooting.projectileAmmo++; // switch this to check how many player have killed
                Destroy(hitCollerer.gameObject);
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
        }   

    }
    
    public void OnLook(InputValue lookValue)
    {
        rightStickPosition = lookValue.Get<Vector2>();

        if (rightStickPosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(rightStickPosition.x, rightStickPosition.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
