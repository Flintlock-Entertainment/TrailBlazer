using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealedInfoCondition : ScriptableCondition
{
    public override string GetDescription()
    {
        string desc =
            "str= " + GetStat(Abilities.Strength) + "  " + "HP= " + GetCurrentHP() + "/" + GetHP() + "\n" +
            "dex= " + GetStat(Abilities.Dexterity) + "  " + "speed= " + GetSpeed() + "\n" +
            "con= " + GetStat(Abilities.Constitution) + "  " + "AC= " + GetAC() + "\n" +
            "int= " + GetStat(Abilities.Intelligence) + "  " + "Reflex= " + GetReflexSave() + "\n" +
            "wis= " + GetStat(Abilities.Wisdom) + "  " + "Fortitude= " + GetFortitueSave() + "\n" +
            "cha= " + GetStat(Abilities.Charisma) + "  " + "Will= " + GetWillSave() + "\n";

        return desc;
    }
}
