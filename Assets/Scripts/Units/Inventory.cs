using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public List<ItemLogic> Items = new List<ItemLogic>();
    public int totalBulk;

    public void AddItem(ItemLogic item, int strengthStat)
    {
        Items.Add(item);
        //totalBulk = Items.Sum(_item => item.itemData.Bulk);
    }
    public void RemoveItem(ItemLogic item, int strengthStat)
    {
        Items.Remove(item);
        //totalBulk = Items.Sum(_item => item.itemData.Bulk);
    }
    


}
