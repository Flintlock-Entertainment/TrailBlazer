using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public List<Item> Items = new List<Item>();
    public int totalBulk;

    public void AddItem(Item item, int strengthStat)
    {
        Items.Add(item);
        totalBulk = Items.Sum(_item => item.itemData.Bulk);
    }
    public void RemoveItem(Item item, int strengthStat)
    {
        Items.Remove(item);
        totalBulk = Items.Sum(_item => item.itemData.Bulk);
    }
    


}
