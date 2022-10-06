using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float timer;
    public float startTime = 300f;
    public float endTime;
    public PlayerStats playerStats;

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
        
        if (timer <= 0)
        {
            GameLose();
        }
        if (playerStats.meter >= 100)
        {
           
            GameWin();
            
        }  
    }
    public void GameLose()
    {              
       SceneManager.LoadScene("LoseScene");
        timer = startTime;
        Destroy(this.gameObject);
    }
   
    public void GameWin()
    {             
       SceneManager.LoadScene("WinScene");       
    }

    public float GetTimer()
    {
        return timer;
    }

}
