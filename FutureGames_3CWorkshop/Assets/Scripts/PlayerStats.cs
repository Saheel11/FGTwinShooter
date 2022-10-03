using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int meter;
    
    // Added these four for testing purposes
    public int increaseMeter; 
    public int decreaseMeter; 
    public int maxMeter = 100;
    public int minMeter = 0;
    
    
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
            canDash = false;
        }
        else
        {
            dashCooldown = 0;
            canDash = true;
        }

        // Added these two for testing purposes
        if (meter > maxMeter)
        {
            meter = maxMeter;
        }
        if (meter < minMeter)
        {
            meter = minMeter;
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
