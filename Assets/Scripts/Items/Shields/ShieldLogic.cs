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
        if (user.unitData is ShieldRaisedCondition)
            return;
        shieldData = (ScriptableWearableItem)itemData;
        MenuManager.Instance.AddLog($"{user.UnitName} raised his shield (+ {shieldData.GetAC(user.unitData.GetStat(Abilities.Dexterity))} AC)\n");
        ShieldRaisedCondition condition = (ShieldRaisedCondition)ScriptableObject.CreateInstance(typeof(ShieldRaisedCondition));
        condition.shield = shieldData;
        condition.duration = ConditionDuration.StartOfTurn;
        user.AddCondition(condition);
        user.UpdateTurns(1);
    }
}
