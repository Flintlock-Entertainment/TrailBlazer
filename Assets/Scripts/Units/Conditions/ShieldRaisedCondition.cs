using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRaisedCondition : ScriptableCondition
{
    public ScriptableWearableItem shield;
    public override int GetAC()
    {
        if (baseUnit is ShieldRaisedCondition)
            return baseUnit.GetAC();
        return baseUnit.GetAC() + shield.GetAC(baseUnit.GetStat(Abilities.Dexterity));
    }

}
