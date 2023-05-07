using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Scriptable Item")]
public class ScriptableItem : ScriptableObject
{
    public Sprite ItemSprite;
    public string itamName;
    public int Bulk;
    public int Price;
    public int Hands;
    public int Hardness;
    public int Durability;
    public int numOfActions;
    public Rarity rarity;
    public string Description;
}

public enum Rarity
{
    Common,
    UnCommon,
    Rare
}