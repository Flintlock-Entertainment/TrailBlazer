using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Range Weapon", menuName = "Weapon/Range/Scriptable Range Weapon")]
public class ScriptableRangeWeapon : ScriptableWeapon
{
    public int AmmoCount;
    public int ReloadTime;
    public ScriptableAmmo Loaded;

    public override int GetAttackRoll()
    {
        if (AmmoCount > 0 && Loaded != null)
        {
            AmmoCount--;
            return base.GetAttackRoll();
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
