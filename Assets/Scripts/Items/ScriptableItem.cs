using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Item")]
public class ScriptableItem : ScriptableObject
{
    public new string name;
    public int Bulk;
    public int Price;
    public int Hands;
    public int Hardness;
    public int Durability;
    public Rarity rarity;
    public string Description;
    public virtual int Use(GameObject parent) {return 0;}
}

public enum Rarity
{
    Common,
    UnCommon,
    Rare
}
