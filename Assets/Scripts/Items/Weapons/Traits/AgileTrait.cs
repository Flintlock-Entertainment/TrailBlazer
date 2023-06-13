using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Item/Weapon/Trait/Agile Trait")]
public class AgileTrait : ScriptableWeaponTrait
{

    public override int GetAttackRoll(BaseUnit user)
    {
        MenuManager.Instance.AddLog($"{user.unitData.GetStat(Abilities.Strength)}(str) + {user.unitData.GetWeaponProf()}(prof) + {GetMultipleAttackPenalty(user.numOfAttacks)} +");
        int roll = Utils.CheckRoll();
        MenuManager.Instance.AddLog($" = {roll + user.unitData.GetStat(Abilities.Strength) + user.unitData.GetWeaponProf() + GetMultipleAttackPenalty(user.numOfAttacks) }\n");

        return roll + user.unitData.GetStat(Abilities.Strength) + user.unitData.GetWeaponProf() + GetMultipleAttackPenalty(user.numOfAttacks);
    }
    public override int GetMultipleAttackPenalty(int numOfAction)
    {
        return (Mathf.Min(numOfAction, 3) - 1) * -4;
    }
}
