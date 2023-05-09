using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableWeapon : ScriptableItem
{
    public int DiceNumber;
    public Func<int> DamageDice;
    public int Range;

    private void OnEnable()
    {
        DamageDice = Utils.GenerateDice(DiceNumber);
    }
    public virtual int GetAttackRoll(BaseUnit user)
    {
        return Utils.CheckRoll() + user.unitData.GetStat(Abilities.Strength) + user.unitData.GetWeaponProf() + GetMultipleAttackPenalty(user.numOfAttacks);
    }
    public virtual int GetCritSuccessDamage(BaseUnit user)
    {
        return DamageDice() + DamageDice() + 2 * user.unitData.GetStat(Abilities.Strength);
    }
    public virtual int GetSuccessDamage(BaseUnit user)
    {
        return DamageDice() + user.unitData.GetStat(Abilities.Strength);
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
