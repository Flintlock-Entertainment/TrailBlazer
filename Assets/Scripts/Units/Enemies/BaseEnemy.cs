using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    protected IBaseStrategy Strategy;

    // Define an enemy's turn as a coroutine so it can be paused and resumed
    public virtual IEnumerator EnemyTurn()
    {
        ResetTurn();
        yield return Strategy.RunStrategy(this); // Call the Move() method
        yield return new WaitForSeconds(0.5f); // Pause for half a second
    }

    // Override the base method for changing turns
    protected override void ChangeTurn()
    {
        GameManager.Instance.ChangeState(GameState.PlayersTurn); // Change the game state to the player's turn
    }
}