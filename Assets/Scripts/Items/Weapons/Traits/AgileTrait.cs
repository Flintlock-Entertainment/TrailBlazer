using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Item/Weapon/Trait/Agile Trait")]
public class AgileTrait : ScriptableWeaponTrait
{
    public override int GetMultipleAttackPenalty(int numOfAction)
    {
        return (Mathf.Min(numOfAction, 3) - 1) * -4;
    }
}
