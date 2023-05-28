using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Scriptable Item")]
public class ScriptableItem : ScriptableObject
{
    [SerializeField] public ItemLogic Prefab;
    [SerializeField] public Sprite ItemSprite;
    [SerializeField] private string itamName;
    [SerializeField] private int Bulk;
    [SerializeField] private int Price;
    [SerializeField] private int Hands;
    [SerializeField] private int Hardness;
    [SerializeField] private int Durability;
    [SerializeField] private int numOfActions;
    [SerializeField] private Rarity rarity;
    [SerializeField] private string Description;

    public virtual ItemLogic GetItem()
    {
        return Prefab;
    }

    public virtual int GetNumOfActions()
    {
        return numOfActions;
    }
}

public enum Rarity
{
    Common,
    UnCommon,
    Rare
}
