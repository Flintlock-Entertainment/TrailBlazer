using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Item/Weapon/Trait/Fatal Trait")]
public class FatalTrait : ScriptableWeaponTrait
{
    public override int GetCritSuccessDamage(BaseUnit user)
    {
        return DamageDice() + DamageDice() + 2 * user.unitData.GetStat(Abilities.Strength);
    }
}
