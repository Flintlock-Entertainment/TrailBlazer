using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BaseCharacter : BaseUnit
{

    public Tile selectedTile;
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        MenuManager.Instance.UpdateMenu(this);
    }

    public void StrideAction()
    {
        if (GameManager.Instance.GameState != GameState.PlayersTurn)
            return;

        var distMatrix = BFS.GetDistanceMatrix(OccupiedTile);
        var tiles = distMatrix.Where(t => t.Value <= speed);
        foreach(var tile in tiles)
        {
            tile.Key.DarkLight(true);
        }
        selectedTile = null;
        StartCoroutine(WaitForPlayerToSelect(tiles, _StrideAction));
    }

    private void _StrideAction(IEnumerable<KeyValuePair<Tile, int>> tiles)
    {
        selectedTile.SetUnit(this);
        foreach (var tile in tiles)
        {
            tile.Key.DarkLight(false);
        }
    }


    public void AttackAction()
    {
        if (GameManager.Instance.GameState != GameState.PlayersTurn)
            return;
        var distMatrix = BFS.GetDistanceMatrix(OccupiedTile);
        var tiles = distMatrix.Where(t => t.Value <= attackRange);
        foreach (var tile in tiles)
        {
            tile.Key.DarkLight(true);
        }
        selectedTile = null;
        StartCoroutine(WaitForPlayerToSelect(tiles, _AttackAction));
    }

    private void _AttackAction(IEnumerable<KeyValuePair<Tile, int>> tiles)
    {
        foreach (var enemy in UnitManager.Instance.Enemies)
        {
            if ((Vector2)enemy.GetCurrentPosition() == selectedTile.getPosition())
                enemy.TakeDamage(damage);
        }
        foreach (var tile in tiles)
        {
            tile.Key.DarkLight(false);
        }
    }

    private IEnumerator WaitForPlayerToSelect(IEnumerable<KeyValuePair<Tile, int>> tiles, Action<IEnumerable<KeyValuePair<Tile, int>>> action)
    {
        while(selectedTile == null)
            yield return null;
        action(tiles);
        ChangeTurn();
    }

    protected override void ChangeTurn()
    {
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }
}
