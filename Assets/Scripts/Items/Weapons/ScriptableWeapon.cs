using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Weapon")]
public abstract class ScriptableWeapon : ScriptableItem
{
    public int DiceNumber;
    public Func<int> DamageDice;
    public int Range;
    public ScriptableWeapon()
    {
        DamageDice = Utils.GenerateDice(DiceNumber);
    }

    public virtual int GetAttackRoll()
    {
        return Utils.d20();
    }
    public virtual int GetCritSuccessDamage()
    {
        return DamageDice() + DamageDice();
    }
    public virtual int GetSuccessDamage()
    {
        return DamageDice();
    }
    public virtual int GetCritFailDamage()
    {
        return 0;
    }
    public virtual int GetFailDamage()
    {
        return 0;
    }
    public virtual int GetMultipleAttackPenalty(int numOfAction)
    {
        return (Mathf.Min(numOfAction, 3) - 1 )*5; 
    }

}
