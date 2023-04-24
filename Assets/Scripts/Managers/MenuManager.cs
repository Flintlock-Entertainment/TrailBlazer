using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;

    [SerializeField] private Menu _menu;
    void Awake() {
        Instance = this;
    }

    public void setupMenu(BaseCharacter character)
    {
        _menu.init(character.HP, character.speed);
    }

    public void UpdateMenu(BaseCharacter character)
    {
        _menu.ChangeHP(character.HP);
    }
    public void StrideAction()
    {
        UnitManager.Instance.Character.StrideAction();
    }

    public void AttackAction()
    {
        UnitManager.Instance.Character.AttackAction();
    }

}
