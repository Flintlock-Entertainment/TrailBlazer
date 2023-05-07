using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  IBaseStrategy : ScriptableObject
{
    public virtual IEnumerator RunStrategy(BaseEnemy user) => throw new NotImplementedException();
}
