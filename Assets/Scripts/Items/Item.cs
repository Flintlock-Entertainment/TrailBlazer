using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ScriptableItem itemData;

    public virtual int Use(BaseEnemy user)
    {
        return 0;
    }
    public virtual int Use(BaseCharacter user)
    {
        return 0;
    }
}
