using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Range Weapon", menuName = "Item/Weapon/Range/Scriptable Range Weapon")]
public class ScriptableRangeWeapon : ScriptableWeapon
{
    public int AmmoCount;
    public int ReloadTime;
    public ScriptableAmmo Loaded;

    public override int GetAttackRoll(BaseUnit user)
    {
        if (AmmoCount > 0 && Loaded != null)
        {
            AmmoCount--;
            return base.GetAttackRoll(user);
        }
        return -1;
    }

    public virtual int LoadWeapon(ScriptableAmmo ammo)
    {
        if(!Loaded && AmmoCount > 0)
        {
            Loaded = ammo;
            return ReloadTime;
        }
        return -1;
    }
}
