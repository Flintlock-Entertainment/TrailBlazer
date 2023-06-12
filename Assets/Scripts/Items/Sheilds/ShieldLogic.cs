using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLogic : ItemLogic
{
    // This variable holds the data of the currently equipped weapon.
    private static ScriptableWearableItem shieldData;
    // This variable holds the instance of WeaponLogic. The "new" keyword is used to hide the base class's Instance property.
    public static new ShieldLogic Instance;

    public override void Use(BaseEnemy user, ScriptableItem itemData)
    {
        use(user, itemData);
    }

    public override void Use(BaseCharacter user, ScriptableItem itemData)
    {
        use(user, itemData);
    }

    private void use(BaseUnit user, ScriptableItem itemData)
    {
        shieldData = (ScriptableWearableItem)itemData;
        ShieldRaisedCondition condition = (ShieldRaisedCondition)ScriptableObject.CreateInstance(typeof(ShieldRaisedCondition));
        condition.shield = shieldData;
        condition.duration = ConditionDuration.StartOfTurn;
        user.AddCondition(condition);
        user.UpdateTurns(1);
    }
}
