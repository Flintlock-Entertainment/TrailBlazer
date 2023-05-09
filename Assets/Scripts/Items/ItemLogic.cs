using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
   public static ItemLogic Instance { get; private set; }

    public void Awake()
    {
        Debug.Log("awake");

        if ((Instance != null) && (Instance != this) && (Instance.GetType() == this.GetType()))
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public ItemLogic getInstance()
    {
        return Instance;
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
        
        ItemLogic item;
        var _instance = Instantiate(itemData.GetItem());
        item = _instance.getInstance();
        return item;
    }
}
