using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public IBaseStrategy Strategy;

    private void OnDestroy()
    {
        UpdateTurns(Turns);
        UnitManager.Instance.RemoveEnemy(this);
    }
    // Define an enemy's turn as a coroutine so it can be paused and resumed
    public virtual IEnumerator EnemyTurn()
    {
        Debug.Log("Start enemy turn");
        ResetTurn();
        yield return Strategy.RunStrategy(this); // Call the Move() method
        yield return new WaitForSeconds(1f); // Pause for half a second
    }

    // Override the base method for changing turns
    protected override void ChangeTurn()
    {
        CombatManager.Instance.ChangeState(CombatState.PlayersTurn); // Change the game state to the player's turn
    }
}