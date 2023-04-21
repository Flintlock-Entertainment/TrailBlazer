using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    public virtual void EnemyTurn()
    {
        //Move();
        //Attack();
    }

    protected void Move()
    {
        var currentPosition = GetCurrentPosition();
        currentPosition += new Vector3(Random.Range(-5, -5), Random.Range(-5, -5), 0);
        GridManager.Instance.GetTileAtPosition((Vector2)currentPosition).SetUnit(this);
    }
    protected void Attack()
    {
        var currentPosition = GetCurrentPosition();
        foreach (BaseCharacter character in UnitManager.Instance.Characters)
        {
            var characterPosition = character.GetCurrentPosition();
            if (Mathf.Abs((float)(characterPosition.x -currentPosition.x)) + Mathf.Abs((float)(characterPosition.y - currentPosition.y)) > 10f)
            {
                Destroy(character);
            }
        }
    }
}
