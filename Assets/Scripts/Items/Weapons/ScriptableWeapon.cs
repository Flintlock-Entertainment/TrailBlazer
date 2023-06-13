using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableWeapon : ScriptableItem
{
    [SerializeField] private int DiceNumber;
    [SerializeField] private int Range;

    private void OnEnable()
    {
        DamageDice = Utils.GenerateDice(DiceNumber);
    }

    [SerializeField] public Func<int> DamageDice { get; private set; }

    public virtual int GetAttackRoll(BaseUnit user)
    {
        MenuManager.Instance.AddLog($"{user.unitData.GetStat(Abilities.Strength)}(str) + {user.unitData.GetWeaponProf()}(prof) + {GetMultipleAttackPenalty(user.numOfAttacks)}(current penalty) +");
        int roll = Utils.CheckRoll();
        MenuManager.Instance.AddLog($" = {roll + user.unitData.GetStat(Abilities.Strength) + user.unitData.GetWeaponProf() + GetMultipleAttackPenalty(user.numOfAttacks) }\n");

        return roll + user.unitData.GetStat(Abilities.Strength) + user.unitData.GetWeaponProf() + GetMultipleAttackPenalty(user.numOfAttacks);
    }
    public virtual int GetCritSuccessDamage(BaseUnit user)
    {
        MenuManager.Instance.AddLog($"Damage:");
        int dmg1 = DamageDice();
        MenuManager.Instance.AddLog(" +");
        int dmg2 = DamageDice();
        MenuManager.Instance.AddLog($" + 2*{user.unitData.GetStat(Abilities.Strength)}(str)");
        MenuManager.Instance.AddLog($" = {dmg1 + dmg2 + 2 * user.unitData.GetStat(Abilities.Strength) }\n");

        return dmg1 + dmg2 + 2 * user.unitData.GetStat(Abilities.Strength);
    }
    public virtual int GetSuccessDamage(BaseUnit user)
    {
        MenuManager.Instance.AddLog($"Damage:");
        int dmg1 = DamageDice();
        MenuManager.Instance.AddLog($" + {user.unitData.GetStat(Abilities.Strength)}(str)");
        MenuManager.Instance.AddLog($" = {dmg1 + user.unitData.GetStat(Abilities.Strength) }\n");
        return dmg1 + user.unitData.GetStat(Abilities.Strength);
    }
    public virtual int GetCritFailDamage(BaseUnit user)
    {
        return 0;
    }
    public virtual int GetFailDamage(BaseUnit user)
    {
        return 0;
    }
    public virtual int GetMultipleAttackPenalty(int numOfAction)
    {
        return (Mathf.Min(numOfAction, 3) - 1 )*-5; 
    }

    public virtual int GetRange()
    {
        return Range;
    }

}
