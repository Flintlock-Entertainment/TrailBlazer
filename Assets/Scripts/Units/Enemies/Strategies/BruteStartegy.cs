using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteStartegy : IBaseStrategy
{
    public IEnumerator RunStrategy(BaseEnemy user)
    {
        while (user.Turns > 0)
        {
            yield return new WaitForSeconds(0.5f);
            // Get the shortest path to the player character
            var path = BFS.GetShortestPath(user.OccupiedTile, UnitManager.Instance.Character.OccupiedTile);
            Weapon wieldedWeapon = (Weapon)user.unitData.MainHand;
            // If the player character is within attack range, attack them
            if (path.Count <= wieldedWeapon.itemData.Range)
            {
                user.UpdateTurns(wieldedWeapon.Use(user));
                continue;
            }
            // If the player character is within moving range, move towards them
            if (path.Count <= user.unitData.Speed + 1)
            {
                path.Last.Value.SetUnit(user); // Set the enemy's position to the last tile in the path
            }
            // If the player character is out of range, move as far as possible towards them
            else
            {
                Debug.Log("out of range");
                Tile[] _path = new Tile[path.Count];
                path.CopyTo(_path, 0);
                _path[user.unitData.Speed].SetUnit(user); // Set the enemy's position to the tile `speed` spaces away from their current position
            }
            user.UpdateTurns(1);
        }
    }
}
