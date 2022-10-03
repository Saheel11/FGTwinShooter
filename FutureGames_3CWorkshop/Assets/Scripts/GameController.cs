using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float timer;
    public float startTime = 300f;
    public float endTime;
    
    
    //public int min = Mathf.FloorToInt(timer / 60); testing if we can turn them into minutes and seconds
    //public int sec = Mathf.FloorToInt(timer % 60);
    


    private void Start()
    {
        timer = startTime;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= endTime)
        {
            timer = endTime;
        }
        
    }
}
