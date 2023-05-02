using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // GameManager is a singleton, so we store a reference to it in Instance
    public static GameManager Instance;

    // GameState stores the current state of the game
    public GameState gameState;

    // Set the GameManager instance to this GameManager on Awake()
    void Awake()
    {
        Instance = this;
    }

    // Start the game by changing the GameState to GenerateGrid
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    // Change the state of the game to newState
    public void ChangeState(GameState newState)
    {
        // Set the GameState to newState
        gameState = newState;

        // Use a switch statement to determine which action to take based on the GameState
        switch (newState)
        {
            case GameState.GenerateGrid:
                // Call the GenerateGrid method in the GridManager
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnCharacter:
                // Call the SpawnCharacter method in the UnitManager
                UnitManager.Instance.SpawnCharacter();
                break;
            case GameState.SpawnEnemies:
                // Call the SpawnEnemies method in the UnitManager
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.PlayersTurn:
                // Do nothing, the game is waiting for player input
                break;
            case GameState.EnemiesTurn:
                // Start the EnemiesTurn coroutine in the UnitManager
                StartCoroutine(UnitManager.Instance.EnemiesTurn());
                break;
            default:
                // If the GameState is not recognized, throw an exception
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    // Check if it is currently the player's turn
    public bool isPlayersTurn()
    {
        return gameState == GameState.PlayersTurn;
    }

    // Check if it is currently the enemy's turn
    public bool isEnemiesTurn()
    {
        return gameState == GameState.EnemiesTurn;
    }
}

// Define an enum to represent the different states of the game

public enum GameState
{
    GenerateGrid = 0,
    SpawnCharacter = 1,
    SpawnEnemies = 2,
    PlayersTurn = 3,
    EnemiesTurn = 4
}