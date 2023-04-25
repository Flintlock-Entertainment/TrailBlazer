using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;

    public int HP;
    public int speed;
    public int damage;
    public int attackRange;
    public Vector3 GetCurrentPosition()
    {
        return OccupiedTile.getPosition();
    }

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        bool alive = HP > 0;
        if (!alive)
            Destroy(this.gameObject);
    }

    protected virtual void ChangeTurn() { }
}
