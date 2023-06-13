using System;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // GameManager is a singleton, so we store a reference to it in Instance
    public static CombatManager Instance;

    // GameState stores the current state of the game
    public CombatState gameState;

    // Set the GameManager instance to this GameManager on Awake()
    void Awake()
    {
        Instance = this;
    }

    // Start the game by changing the GameState to GenerateGrid
    void Start()
    {
        ChangeState(CombatState.GenerateGrid);
    }

    // Change the state of the game to newState
    public void ChangeState(CombatState newState)
    {
        // Set the GameState to newState
        gameState = newState;

        // Use a switch statement to determine which action to take based on the GameState
        switch (newState)
        {
            case CombatState.GenerateGrid:
                // Call the GenerateGrid method in the GridManager
                GridManager.Instance.GenerateGrid();
                break;
            case CombatState.SpawnCharacter:
                // Call the SpawnCharacter method in the UnitManager
                UnitManager.Instance.SpawnCharacter();
                break;
            case CombatState.SpawnEnemies:
                // Call the SpawnEnemies method in the UnitManager
                UnitManager.Instance.SpawnEnemies();
                break;
            case CombatState.PlayersTurn:
                UnitManager.Instance.PlayerTurn();
                break;
            case CombatState.EnemiesTurn:
                // Start the EnemiesTurn coroutine in the UnitManager
                StartCoroutine(UnitManager.Instance.EnemiesTurn());
                break;
            case CombatState.Win:
                GameManager.Instance.LoadScene("MainMap");
                break;
            case CombatState.Lose:
                GameManager.Instance.LoadScene("Death");
                break;
            default:
                // If the GameState is not recognized, throw an exception
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    // Check if it is currently the player's turn
    public bool isPlayersTurn()
    {
        return gameState == CombatState.PlayersTurn;
    }

    // Check if it is currently the enemy's turn
    public bool isEnemiesTurn()
    {
        return gameState == CombatState.EnemiesTurn;
    }
}

// Define an enum to represent the different states of the game

public enum CombatState
{
    GenerateGrid = 0,
    SpawnCharacter = 1,
    SpawnEnemies = 2,
    PlayersTurn = 3,
    EnemiesTurn = 4,
    Win = 5,
    Lose = 6
}