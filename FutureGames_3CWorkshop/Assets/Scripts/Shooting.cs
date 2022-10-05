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

    public UIController uiController;
    
  

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        GameObject[] ui = GameObject.FindGameObjectsWithTag("UI");
        foreach(GameObject go in ui)
        {
            if(go.GetComponent<UIController>() != null)
            {
                uiController = go.GetComponent<UIController>();

            }
           //uiController = go.GetComponent<UIController>();

        }
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
            projectileClone.GetComponent<SphereCollider>().radius = 1;
            
            //projectileAmmo--;
            IncreaseAmmo(-1);
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

    public void IncreaseAmmo(int ammoincrease)
    {



        projectileAmmo = Mathf.Clamp(projectileAmmo, 0, 6);

        projectileAmmo += ammoincrease;
        uiController.ammoShow(projectileAmmo);


    }

}
