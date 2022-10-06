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

    public UIController ui;
   
    
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
