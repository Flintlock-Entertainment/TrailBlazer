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
    [SerializeField] private InformationMenu _menu;

    [SerializeField] private CombatLogger _logger;

    [SerializeField]
    private Button MainHand, OffHand;

    // This method is called when the script instance is being loaded.
    void Awake()
    {
        // Assign this instance to the public static Instance field.
        Instance = this;
    }

    public void setupMenu(BaseCharacter character)
    {
        MainHand.gameObject.GetComponent<Image>().sprite = character.unitData.GetMainHand().ItemSprite;
        OffHand.gameObject.GetComponent<Image>().sprite = character.unitData.GetOffHand().ItemSprite;
        _menu.init(character.unitData.GetDescription(), character.Turns);
    }

    public void UpdateMenu(BaseCharacter character)
    {
        _menu.init(character.unitData.GetDescription(), character.Turns);
    }

    public void ShowInformation(string desc)
    {
        _menu.ShowInformation(desc);
    }

    public void AddLog(string log)
    {
        _logger.AddLog(log);
    }
}
