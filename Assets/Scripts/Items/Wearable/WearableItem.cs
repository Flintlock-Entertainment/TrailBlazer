using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearableItem : Item
{
    public new ScriptableWearableItem itemData;

    public int GetAC(int dex)
    {
        return itemData.ACBonus + Mathf.Min(dex, itemData.DexCap);
    }
}
