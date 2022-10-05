using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float timer;
    public float startTime = 300f;
    public float endTime;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    
    //public int min = Mathf.FloorToInt(timer / 60); testing if we can turn them into minutes and seconds
    //public int sec = Mathf.FloorToInt(timer % 60);
    


    private void Start()
    {
        timer = startTime;
        ChangeGameState(GameState.Startscreen);
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= endTime)
        {
            timer = endTime;
        }
        
    }
    public enum GameState
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
                break;
            case GameState.Play:
                HandlePlay();
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

}
