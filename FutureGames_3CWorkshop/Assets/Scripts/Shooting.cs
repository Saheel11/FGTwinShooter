using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    
    public GameObject projectile;
    
    public float projectileSpeed;
    
    public int projectileAmmo;
    
    public Vector2 rightStickPosition;

    public AudioSource audioSource;
    public AudioClip shootingClip;

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
    }

    public void OnFire()
    {
        if (projectileAmmo > 0)
        {
            GameObject projectileClone = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
            projectileClone.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
            projectileClone.GetComponent<Projectile>().whatIHit = "Enemy";
            projectileAmmo--;
            audioSource.clip = shootingClip;
            audioSource.Play();
            Destroy(projectileClone, 2f);

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
