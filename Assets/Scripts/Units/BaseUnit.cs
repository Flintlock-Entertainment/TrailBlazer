using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;

    public ScriptableUnit unitData;
    public Inventory inventory;
    public Vector3 GetCurrentPosition()
    {
        return OccupiedTile.getPosition();
    }

    public virtual void TakeDamage(int damage)
    {
        unitData.HP -= damage;
        bool alive = unitData.HP > 0;
        if (!alive)
            Destroy(this.gameObject);
    }

    protected virtual void ChangeTurn() { }
}
