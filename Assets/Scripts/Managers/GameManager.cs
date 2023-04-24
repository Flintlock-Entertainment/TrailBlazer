using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/f5pm29yhaTs
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnCharacter:
                UnitManager.Instance.SpawnCharacter();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.PlayersTurn:
                break;
            case GameState.EnemiesTurn:
                StartCoroutine(UnitManager.Instance.EnemiesTurn());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnCharacter = 1,
    SpawnEnemies = 2,
    PlayersTurn = 3,
    EnemiesTurn = 4
}
