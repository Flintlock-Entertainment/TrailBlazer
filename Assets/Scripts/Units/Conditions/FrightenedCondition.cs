using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightenedCondition : ScriptableCondition
{
    public override int GetStat(Abilities ability)
    {
        return baseUnit.GetStat(ability) - conditionLevel;
    }
}
