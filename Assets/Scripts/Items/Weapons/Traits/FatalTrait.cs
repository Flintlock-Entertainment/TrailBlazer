using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatalTrait : ScriptableWeaponTrait
{
    public override int GetCritSuccessDamage()
    {
        return DamageDice() + DamageDice();
    }
}
