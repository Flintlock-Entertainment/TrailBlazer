using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableCondition : ScriptableUnit
{
    public ScriptableUnit baseUnit;

    [SerializeField] public int conditionLevel;
    [SerializeField] public ConditionDuration duration;

    public override int GetReflexSave()
    {
        return baseUnit.GetReflexSave();
    }

    public override int GetFortitueSave()
    {
        return baseUnit.GetFortitueSave();
    }

    public override int GetWillSave()
    {
        return baseUnit.GetWillSave();
    }

    public override int GetStat(Abilities ability)
    {
        return baseUnit.GetStat(ability);
    }

    public override int GetActionsPerTurn()
    {
        return baseUnit.GetActionsPerTurn();
    }

    public override int GetHP()
    {
        return baseUnit.GetHP();
    }

    public override int GetCurrentHP()
    {
        return baseUnit.GetCurrentHP();
    }

    public override void UpdateCurrentHP(int update)
    {
        baseUnit.UpdateCurrentHP(update);
    }
    public override int GetAC()
    {
        return baseUnit.GetAC();
    }

    public override int GetSpeed()
    {
        return baseUnit.GetSpeed();
    }

    public override int GetCoins()
    {
        return baseUnit.GetCoins();
    }

    public override void UpdateCoins(int update)
    {
        baseUnit.UpdateCoins(update);
    }

    public override int GetLevel()
    {
        return baseUnit.GetLevel();
    }
    public override int GetWeaponProf()
    {
        return baseUnit.GetWeaponProf();
    }

    public override int GetSkill(Skills skill)
    {
        return baseUnit.GetSkill(skill);
    }
    public override ScriptableWeapon GetMainHand()
    {
        return baseUnit.GetMainHand();
    }

    public override ScriptableItem GetOffHand()
    {
        return baseUnit.GetOffHand();
    }

    public override void RevealInfo()
    {
        baseUnit.RevealInfo();
    }
    public override bool GetRevealInfo()
    {
        return baseUnit.GetRevealInfo();
    }

    public override string GetDescription()
    {
        revealInfo = GetRevealInfo();
        return  base.GetDescription();
    }

    public ConditionDuration GetConditionDuration()
    {
        return duration;
    }


}

public enum ConditionDuration
{
    Custom = 0,
    EndOfTurn = 1,
    StartOfTurn = 2
}
