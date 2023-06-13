using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Item/Weapon/Trait/Fatal Trait")]
public class FatalTrait : ScriptableWeaponTrait
{
    public override int GetCritSuccessDamage(BaseUnit user)
    {
        MenuManager.Instance.AddLog($"Damage:");
        int dmg1 = DamageDice();
        MenuManager.Instance.AddLog(" +");
        int dmg2 = DamageDice();
        MenuManager.Instance.AddLog($" + 2*{user.unitData.GetStat(Abilities.Strength)}(str)");
        MenuManager.Instance.AddLog($" = {dmg1 + dmg2 + 2 * user.unitData.GetStat(Abilities.Strength) }\n");
        return dmg1 + dmg2 + 2 * user.unitData.GetStat(Abilities.Strength);
    }
}
