using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProneCondition : ScriptableCondition
{
    public override int GetActionsPerTurn()
    {
        return base.GetActionsPerTurn() - 1;
    }

    public override int GetAC()
    {
        return base.GetAC() - 2;
    }

    public override int GetSpeed()
    {
        return 0;
    }
}
