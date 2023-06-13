using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// following this tutorial: https://www.youtube.com/watch?v=AoD_F1fSFFg
/*
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] public ScriptableItem[] Items;

    public Transform Itemcontent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ScriptableItem item)
    {
        Items.Add(item);
    }

    public void Remove(ScriptableItem item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, Itemcontent);
            var itemName = obj.transform.Find("Item/ItemName").GetComponant<Text>();
            var itemIcon = obj.transform.Find("Item/ItemIcon").GetComponant<Image>();

            itemName = item.itemName;
            itemIcon = item.icon;
        }
    }

}
*/