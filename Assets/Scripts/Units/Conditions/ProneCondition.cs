using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProneCondition : ScriptableCondition
{
    public override int GetActionsPerTurn()
    {
        return baseUnit.GetActionsPerTurn() - 1;
    }

    public override int GetAC()
    {
        return baseUnit.GetAC() - 2;
    }
    

    public override int GetSpeed()
    {
        return 0;
    }
}
