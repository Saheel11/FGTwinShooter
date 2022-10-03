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
    
    public Vector2 rightStickPosition;

    public bool isShooting;
    
    

    private void Update()
    {
    }

    public void OnFire()
    {
        isShooting = true;
        GameObject projectileClone =
            Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
            projectileClone.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
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
