using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    // Define an enemy's turn as a coroutine so it can be paused and resumed
    public virtual IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.5f); // Pause for half a second
        Move(); // Call the Move() method
        yield return new WaitForSeconds(0.5f); // Pause for half a second
    }

    // Define how the enemy moves
    protected void Move()
    {
        // Get the shortest path to the player character
        var path = BFS.GetShortestPath(OccupiedTile, UnitManager.Instance.Character.OccupiedTile);

        // If the player character is within attack range, attack them
        if (path.Count <= attackRange)
        {
            Debug.Log("in range");
            Attack();
            return;
        }
        // If the player character is within moving range, move towards them
        if (path.Count <= speed + 1)
        {
            Debug.Log("in moving range");
            path.Last.Value.SetUnit(this); // Set the enemy's position to the last tile in the path
            Debug.Log(path.Last.Value);
        }
        // If the player character is out of range, move as far as possible towards them
        else
        {
            Debug.Log("out of range");
            Tile[] _path = new Tile[path.Count];
            path.CopyTo(_path, 0);
            _path[speed].SetUnit(this); // Set the enemy's position to the tile `speed` spaces away from their current position
        }
    }

    // Define how the enemy attacks
    protected void Attack()
    {
        UnitManager.Instance.Character.TakeDamage(damage); // Deal damage to the player character
    }

    // Override the base method for changing turns
    protected override void ChangeTurn()
    {
        GameManager.Instance.ChangeState(GameState.PlayersTurn); // Change the game state to the player's turn
    }
}