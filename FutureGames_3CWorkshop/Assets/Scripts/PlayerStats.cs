using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int meter;
    
    public int increaseMeter; // testing move to player dash
    public int decreaseMeter; // testing move to enemy projectile
    
    
    public float dashCooldown;
    public float dashStartCooldown;
    public bool canDash;
    
    
    void Update()
    {
        //Testing with manual inputs
        if (Input.GetKeyDown(KeyCode.C))
        {
            IncreaseMeter();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DecreaseMeter();
        }
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            StartDashCooldown();
        }
         
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        if (dashCooldown <= 0)
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }
    }


    public void IncreaseMeter()
    {
        meter += increaseMeter;
    }
    
    public void DecreaseMeter()
    {
        meter -= decreaseMeter;
    }

    public void StartDashCooldown()
    {
        dashCooldown = dashStartCooldown;
    }
}
