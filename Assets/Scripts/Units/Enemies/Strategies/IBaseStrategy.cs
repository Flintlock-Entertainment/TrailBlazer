using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IBaseStrategy 
{
    public IEnumerator RunStrategy(BaseEnemy user);
}
