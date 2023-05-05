using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*
This is the base class for all characters in the game.
It inherits from the BaseUnit class.
*/
public class BaseCharacter : BaseUnit
{
    public Tile selectedTile;
    //Override the TakeDamage function from BaseUnit to include updating the menu.
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        MenuManager.Instance.UpdateMenu(this);
    }

    public void MainHand()
    {
        if (!GameManager.Instance.isPlayersTurn()) return;

        Turns -= unitData.MainHand.Use(this);
    }

    /*
     * Perform the Stride action for the character.
     * Shows the tiles that the character can move to and waits for player input to select a tile.
     * Once a tile is selected, moves the character to the tile and ends the turn.
     */
    public void StrideAction()
    {
        // Check if it is the player's turn.
        if (!GameManager.Instance.isPlayersTurn()) return;

        // Get the distance matrix for the current tile and show tiles that are within the character's speed range.
        var distMatrix = BFS.GetDistanceMatrix(OccupiedTile);
        var tiles = distMatrix.Where(t => t.Value <= unitData.Speed);
        foreach (var tile in tiles)
        {
            tile.Key.DarkLight(true);
        }

        // Reset the selected tile.
        selectedTile = null;

        // Wait for player to select a tile and then move the character to the selected tile.
        StartCoroutine(WaitForPlayerToSelect(tiles, _StrideAction));
    }

    /*
     * Private function that actually performs the Stride action after a tile is selected.
     * Sets the character's position to the selected tile and hides the tiles that were highlighted.
     */
    private void _StrideAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        selectedTile.SetUnit(this);
        foreach (var tile in tiles)
        {
            tile.Key.DarkLight(false);
        }
        Turns--;
    }

    
    // Wait for the player to select a tile and then call the given action with that tile
    public IEnumerator WaitForPlayerToSelect(IEnumerable<KeyValuePair<Tile, int>> tiles, Action<IEnumerable<KeyValuePair<Tile, int>>, BaseCharacter> action)
    {
        while (selectedTile == null)
            yield return null;
        action(tiles, this);
        if(Turns == 0)
            ChangeTurn();
    }

    // Change the turn to the enemies' turn
    protected override void ChangeTurn()
    {
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }
}