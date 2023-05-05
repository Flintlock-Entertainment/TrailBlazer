using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsMenu : MonoBehaviour
{
    // This method is called when the player performs a stride action.
    public static void StrideAction()
    {
        // Call the StrideAction method of the character controlled by the UnitManager.
        UnitManager.Instance.Character.StrideAction();
    }

    // This method is called when the player performs an action with an Item he hold in the MainHand.
    public static void MainHandAction()
    {
        // Call the AttackAction method of the character controlled by the UnitManager.
        UnitManager.Instance.Character.MainHand();
    }
}
