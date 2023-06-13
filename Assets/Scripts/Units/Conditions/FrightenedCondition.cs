using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightenedCondition : ScriptableCondition
{
    public override int GetReflexSave()
    {
        return baseUnit.GetReflexSave() -1;
    }

    public override int GetFortitueSave()
    {
        return baseUnit.GetFortitueSave() -1;
    }

    public override int GetWillSave()
    {
        return baseUnit.GetWillSave() -1;
    }

    public override int GetStat(Abilities ability)
    {
        return baseUnit.GetStat(ability) - 1;
    }

    public override int GetAC()
    {
        return baseUnit.GetAC() -1;
    }

   
    public override int GetSkill(Skills skill)
    {
        return baseUnit.GetSkill(skill) -1;
    }
}
