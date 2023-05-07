using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
   public static ItemLogic Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }
    public virtual int Use(BaseEnemy user, ScriptableItem itemData)
    {
        return 0;
    }
    public virtual int Use(BaseCharacter user, ScriptableItem itemData)
    {
        return 0;
    }

    public static ItemLogic GetItemLogic(ScriptableItem itemData)
    {
        Debug.Log("get item logic");
        ItemLogic item;  
        Instantiate(itemData.GetItem());
        item = Instance;
        return item;
    }
}
