using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is a class named MenuManager that is inherited from MonoBehaviour.
public class MenuManager : MonoBehaviour
{
    // This is a public static instance of MenuManager that can be accessed from any other script.
    public static MenuManager Instance;

    // This is a serialized field for Menu.
    [SerializeField] private Menu _menu;

    // This method is called when the script instance is being loaded.
    void Awake()
    {
        // Assign this instance to the public static Instance field.
        Instance = this;
    }

    // This method is used to set up the menu with a character's HP and speed.
    public void setupMenu(BaseCharacter character)
    {
        // Initialize the menu with the character's HP and speed.
        _menu.init(character.HP, character.speed);
    }

    // This method is used to update the character's HP in the menu.
    public void UpdateMenu(BaseCharacter character)
    {
        // Update the character's HP in the menu.
        _menu.ChangeHP(character.HP);
    }

    // This method is called when the player performs a stride action.
    public void StrideAction()
    {
        // Call the StrideAction method of the character controlled by the UnitManager.
        UnitManager.Instance.Character.StrideAction();
    }

    // This method is called when the player performs an attack action.
    public void AttackAction()
    {
        // Call the AttackAction method of the character controlled by the UnitManager.
        UnitManager.Instance.Character.AttackAction();
    }
}
