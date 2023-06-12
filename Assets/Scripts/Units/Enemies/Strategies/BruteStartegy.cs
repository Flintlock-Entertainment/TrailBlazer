using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Brute Strategy", menuName = "Strategies/Brute Strategy")]
public class BruteStartegy : IBaseStrategy
{
    public override IEnumerator RunStrategy(BaseEnemy user)
    {
        while (user.Turns > 0)
        {
            yield return new WaitForSeconds(0.5f);
            var target = UnitManager.Instance.Character.OccupiedTile;
            // Get the shortest path to the player character
            var path = BFS.GetShortestPath(user.OccupiedTile, target);

            var weaponData = user.unitData.GetMainHand();

            WeaponLogic wieldedWeapon = (WeaponLogic)ItemLogic.GetItemLogic(weaponData);
            // If the player character is within attack range, attack them
            if (path.Count <= weaponData.GetRange())
            {
                wieldedWeapon.Use(user, weaponData);
                continue;
            }
            // If the player character is within moving range, move towards them
            if (path.Count <= user.unitData.GetSpeed() + 1)
            {
                path.Last.Value.SetUnit(user); // Set the enemy's position to the last tile in the path
            }
            //if(path.Count == 0)
           // {
              //  Debug.Log("unreachable");
              //  var distMatrix = BFS.GetDistanceMatrix(user.OccupiedTile);

//float minDist = CalculateDistance(user.OccupiedTile.getPosition(), target.getPosition());
              //  Tile minDistTile = user.OccupiedTile;
              //  float tempDist;
               // foreach ( var tile in distMatrix)
              //  {
               ///     tempDist = CalculateDistance(tile.Key.getPosition(), target.getPosition());
                 //   if(tempDist < minDist)
                 //   {
                 //       minDist = tempDist;
                 //       minDistTile = tile.Key;
                 //   }
               // }
               // path = BFS.GetShortestPath(user.OccupiedTile, minDistTile);
               // Tile[] _path = new Tile[path.Count];
              //  path.CopyTo(_path, 0);
              //  _path[Mathf.Min(user.unitData.GetSpeed(), path.Count - 1)].SetUnit(user);

           // }
            // If the player character is out of range, move as far as possible towards them
            else
            {
                Debug.Log("out of range");
                Tile[] _path = new Tile[path.Count];
                path.CopyTo(_path, 0);
                _path[user.unitData.GetSpeed()].SetUnit(user); // Set the enemy's position to the tile `speed` spaces away from their current position
            }
            user.UpdateTurns(1);
        }
    }

    private float CalculateDistance(Vector2 pos1, Vector2 pos2)
    {
        float dx = pos1.x - pos2.x;
        float dy = pos1.y - pos2.y;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }
}
