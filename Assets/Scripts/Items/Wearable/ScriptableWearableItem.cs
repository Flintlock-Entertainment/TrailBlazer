using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Wearable/Scriptable WearableItem")]
public class ScriptableWearableItem : ScriptableItem
{
    public int ACBonus;
    public int DexCap;
    public int CheckPenalty;
    public int SpeedPenalty;
    public int StrengthThreshold;

    public int GetAC(int dex)
    {
        return ACBonus + Mathf.Min(dex, DexCap);
    }
}
