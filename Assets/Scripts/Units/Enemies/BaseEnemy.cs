using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public virtual IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        Move();
        yield return new WaitForSeconds(0.5f);
    }

    protected void Move()
    {
        var path = BFS.GetShortestPath(OccupiedTile, UnitManager.Instance.Character.OccupiedTile);
        if(path.Count <= attackRange){
            Debug.Log("in range");
            Attack();
            return;
        }
        if(path.Count <= speed + 1)
        {
            Debug.Log("in moving range");
            path.Last.Value.SetUnit(this);
            Debug.Log(path.Last.Value);
        }
        else
        {
            Debug.Log("out of range");
            Tile[] _path = new Tile[path.Count];
            path.CopyTo(_path, 0);
            _path[speed].SetUnit(this);
        }
        
    }
    protected void Attack()
    {
        UnitManager.Instance.Character.TakeDamage(damage);
    }
    protected override void ChangeTurn()
    {
        GameManager.Instance.ChangeState(GameState.PlayersTurn);
    }
}
