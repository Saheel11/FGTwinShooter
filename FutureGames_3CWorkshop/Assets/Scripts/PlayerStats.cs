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
    }


    public void IncreaseMeter()
    {
        if (meter <= maxMeter)
        {
            meter += increaseMeter;
        }
        
    }
    
    public void DecreaseMeter()
    {
        if (meter > minMeter)
        {
            meter -= decreaseMeter;
        }
        else
        {
            meter = minMeter;
        }
    }

    public void StartDashCooldown()
    {
        dashCooldown = dashStartCooldown;
    }
}
