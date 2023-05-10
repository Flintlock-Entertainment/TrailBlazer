using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemLogic : MonoBehaviour
{
    // Singleton instance
    public static ItemLogic Instance { get; private set; }

    // Called when the script instance is being loaded
    public void Awake()
    {
        // Check if an instance of this object already exists
        if ((Instance != null) && (Instance != this) && (Instance.GetType() == this.GetType()))
        {
            // If it does, destroy this object
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set this object as the instance
            Instance = this;
        }
    }

    // Returns the singleton instance
    public ItemLogic getInstance()
    {
        return Instance;
    }

    // This method is intended to be overridden by derived classes
    public virtual int Use(BaseEnemy user, ScriptableItem itemData)
    {
        // Default implementation returns 0
        return 0;
    }

    // This method is intended to be overridden by derived classes
    public virtual int Use(BaseCharacter user, ScriptableItem itemData)
    {
        // Default implementation returns 0
        return 0;
    }

    // Instantiate an ItemLogic object based on the given ScriptableItem
    public static ItemLogic GetItemLogic(ScriptableItem itemData)
    {
        // Instantiate the ScriptableItem's associated prefab
        var _instance = Instantiate(itemData.GetItem());

        // Get the singleton instance of the ItemLogic object from the prefab
        ItemLogic item = _instance.getInstance();

        // Return the singleton instance
        return item;
    }
}
