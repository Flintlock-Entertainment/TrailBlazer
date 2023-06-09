using System;
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

    [SerializeField] public AudioClip[] HitList;
    [SerializeField] protected AudioSource HitSoundEffect;



    public void init()
    {
        unitData.SetCurrentHP(unitData.GetHP());
        unitData.SetRevealInfo(false);
    }
    public int Turns { get; protected set; } 

    public int numOfAttacks { get; protected set; }

    public virtual void ResetTurn()
    {
        MenuManager.Instance.AddLog($"\n\t{UnitName}'s Turn\n\n");
        Turns = unitData.GetActionsPerTurn();
        numOfAttacks = 0;
        RemoveConditions(ConditionDuration.StartOfTurn, typeof(ScriptableCondition));
    }

    public virtual void UpdateTurns(int update)
    {
        Turns -= update;
        if(Turns == 0)
            RemoveConditions(ConditionDuration.EndOfTurn, typeof(ScriptableCondition));
        MenuManager.Instance.AddLog("\n");
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
            unitData.UpdateCurrentHP(-1*damage);
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
        Debug.Log("RemoveConditions, duration: " + currentDuration);
        while (prev is ScriptableCondition )
        {
            curr = (ScriptableCondition)prev;
            prev = curr.baseUnit;
            Debug.Log("RemoveConditions, test1 : " + (curr is ScriptableCondition) + " test2 :" + (curr.GetConditionDuration() == currentDuration));
            if (curr.GetConditionDuration() == currentDuration && curr is ScriptableCondition)
            {
                Debug.Log("Condition Removed");
                if (next != null)
                    next.baseUnit = prev;
                else
                    unitData = prev;
                
                    
                Destroy(curr);
            }
            else
            {
                next = curr;
            }

        }
    }
    protected virtual void ChangeTurn() { }

    public void RevealInfo()
    {
        unitData.RevealInfo();
    }
}
