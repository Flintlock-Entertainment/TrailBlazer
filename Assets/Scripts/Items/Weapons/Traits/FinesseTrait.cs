using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Item/Weapon/Trait/Finesse Trait")]
public class FinesseTrait : ScriptableWeaponTrait
{
    public override int GetAttackRoll(BaseUnit user)
    {
        return base.GetAttackRoll(user) + user.unitData.GetStat(Abilities.Dexterity) - user.unitData.GetStat(Abilities.Strength);
    }
}
