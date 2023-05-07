using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableWeaponTrait : ScriptableWeapon
{
    public ScriptableWeapon baseWeapon;

    public override int GetAttackRoll(BaseUnit user)
    {
        return baseWeapon.GetAttackRoll( user);
    }
    public override int GetCritSuccessDamage(BaseUnit user)
    {
        return baseWeapon.GetCritSuccessDamage(user);
    }
    public override int GetSuccessDamage(BaseUnit user)
    {
        return baseWeapon.GetSuccessDamage(user);
    }
    public override int GetCritFailDamage(BaseUnit user)
    {
        return baseWeapon.GetCritFailDamage(user);
    }
    public override int GetFailDamage(BaseUnit user)
    {
        return baseWeapon.GetFailDamage(user);
    }
    public override int GetMultipleAttackPenalty(int numOfAction)
    {
        return baseWeapon.GetMultipleAttackPenalty(numOfAction);
    }

    public override int GetRange()
    {
        return base.GetRange();
    }

}
