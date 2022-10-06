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
    

    /*public GameState State; THIS IS FOR GAMESTATES

    public static event Action<GameState> OnGameStateChanged;
    
    //public int min = Mathf.FloorToInt(timer / 60); testing if we can turn them into minutes and seconds
    //public int sec = Mathf.FloorToInt(timer % 60);
    */


    private void Start()
    {
        timer = startTime;
        //ChangeGameState(GameState.Startscreen); THIS IS FOR GAMESTATES
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
        /*if (meter <= 0)
        {
            GameLose();
        }*/
    }

    public void GameLose()
    {              
       SceneManager.LoadScene("LoseScene");       
    }
   
    public void GameWin()
    {             
       SceneManager.LoadScene("WinScene");       
    }

   

    /* public enum GameState THIS IS FOR GAMESTATES
     {
         Startscreen, 
         Play,
         Win,
         Lose
     }
    

    public void ChangeGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Startscreen:
                HandleStartscreen();
                break;
            case GameState.Play:
                //HandlePlay();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged(newState);
        
    }

    private void HandlePlay()
    {
        if (meter >= 100) ChangeGameState(GameState.Win);
        else if (timer <= 0) ChangeGameState(GameState.Lose);
        else ChangeGameState(GameState.Play);

    }
    

    private void HandleStartscreen()
    {

    }
    */


}
