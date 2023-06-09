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

    public int currentHP { get; private set; }

    public void init()
    {
        currentHP = unitData.GetHP();
    }
    public int Turns { get; protected set; } 

    public int numOfAttacks { get; protected set; }

    public virtual void ResetTurn()
    {
        Turns = 3;
        numOfAttacks = 0;
    }

    public virtual void UpdateTurns(int update)
    {
        Turns -= update;
    }

    public void CharacterAttackCounterIncrease()
    {
        numOfAttacks++;
    }

    public Vector3 GetCurrentPosition()
    {
        return OccupiedTile.getPosition();
    }

    public virtual void TakeDamage(int damage)
    {
        if(damage != 0)
        {
            MenuManager.Instance.AddLog($"{UnitName} took {damage} points of damage\n");
            currentHP -= damage;
            bool alive = currentHP > 0;
            if (!alive)
                Destroy(this.gameObject);
        }
        
    }

    public virtual void AddCondition(ScriptableCondition condition)
    {
        condition.baseUnit = unitData;
        unitData = condition;
    }

    public virtual void RemoveConditions(ConditionDuration currentDuration)
    {

        ScriptableCondition next = null;
        ScriptableUnit prev = unitData;
        while (prev.GetType() == typeof(ScriptableCondition))
        {
            ScriptableCondition curr = (ScriptableCondition)prev;
            prev = curr.baseUnit;
            if (curr.GetConditionDuration() == currentDuration)
            {
                next.baseUnit = prev;
            }
            else
            {
                next = curr;
            }

        }
    }
    protected virtual void ChangeTurn() { }
}
