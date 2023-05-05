using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableWeaponTrait : ScriptableWeapon
{
    public ScriptableWeapon baseWeapon;

    public override int GetAttackRoll()
    {
        return baseWeapon.GetAttackRoll();
    }
    public override int GetCritSuccessDamage()
    {
        return baseWeapon.GetCritSuccessDamage();
    }
    public override int GetSuccessDamage()
    {
        return baseWeapon.GetSuccessDamage();
    }
    public override int GetCritFailDamage()
    {
        return baseWeapon.GetCritFailDamage();
    }
    public override int GetFailDamage()
    {
        return baseWeapon.GetFailDamage();
    }
    public override int GetMultipleAttackPenalty(int numOfAction)
    {
        return baseWeapon.GetMultipleAttackPenalty(numOfAction);
    }

}
