using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    private static GameStateManager instance;

    public static GameStateManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameStateManager();

            return instance;
        }
    }
    public GameState CurrentGameState { get; private set; }

    //public delegate void GameStateChangeHandler(GameState newState);
    public static Action<GameState> OnGameStateChanged;

    private GameStateManager()
    {

    }
    public void SetState(GameState state)
    {
        if(state==CurrentGameState)
            return;

        CurrentGameState = state;
        OnGameStateChanged?.Invoke(state);
    }
}
