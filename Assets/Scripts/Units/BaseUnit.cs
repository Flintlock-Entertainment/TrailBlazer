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


    public void init()
    {
        unitData.SetCurrentHP(unitData.GetHP());
    }
    public int Turns { get; protected set; } 

    public int numOfAttacks { get; protected set; }

    public virtual void ResetTurn()
    {
        Turns = unitData.GetActionsPerTurn();
        numOfAttacks = 0;
        RemoveConditions(ConditionDuration.StartOfTurn, typeof(ScriptableCondition));
    }

    public virtual void UpdateTurns(int update)
    {
        Turns -= update;
        if(Turns == 0)
            RemoveConditions(ConditionDuration.EndOfTurn, typeof(ScriptableCondition));
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
            unitData.SetCurrentHP(-1*damage);
            bool alive = unitData.GetCurrentHP() > 0;
            if (!alive)
                Destroy(this.gameObject);
        }
        
    }

    public virtual void AddCondition(ScriptableCondition condition)
    {
        condition.baseUnit = unitData;
        unitData = condition;
    }

    public virtual void RemoveConditions(ConditionDuration currentDuration, System.Type conitionTypeToRemove)
    {

        
        ScriptableUnit prev = unitData;
        ScriptableCondition curr = null, next  = null;
        while (prev.GetType() == typeof(ScriptableCondition))
        {
            curr = (ScriptableCondition)prev;
            prev = curr.baseUnit;
            if (curr.GetConditionDuration() == currentDuration && curr.GetType() == conitionTypeToRemove)
            {
                next.baseUnit = prev;
                Destroy(curr);
            }
            else
            {
                next = curr;
            }

        }
    }
    protected virtual void ChangeTurn() { }
}
