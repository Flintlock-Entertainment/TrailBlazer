using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable WearableItem")]
public class ScriptableWearableItem : ScriptableItem
{
    public int ACBonus;
    public int DexCap;
    public int CheckPenalty;
    public int SpeedPenalty;
    public int StrengthThreshold; 
}
