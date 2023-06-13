using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actions : MonoBehaviour
{
    static public Actions Instance;
    private BaseCharacter character => UnitManager.Instance.Character;
    private Tile selectedTile => GridManager.Instance.selectedTile;

    private void Awake()
    {
        Instance = this;
    }
    public void StrideAction()
    {
        if (!IsPlayersTurn()) return;
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= character.unitData.GetSpeed() && t.Value > 0;

        ChoiceOnMap(discriminator, _StrideAction);
    }

    private void _StrideAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        selectedTile.SetUnit(character);
        if (character.OccupiedTile != selectedTile)
            return;
        AddLog($"{character.UnitName} stride\n");
        user.UpdateTurns(1);
    }

    public void UseMainHand()
    {
        if (!IsPlayersTurn()) return;
        var itemData = character.unitData.GetMainHand();
        ItemLogic.GetItemLogic(itemData).Use(character, itemData);
    }

    public void UseOffHand()
    {
        if (!IsPlayersTurn()) return;
        var itemData = character.unitData.GetOffHand();
        ItemLogic.GetItemLogic(itemData).Use(character, itemData);
    }

    public void Trip()
    {
       if (!IsPlayersTurn()) return;
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= 1 && t.Value > 0;
        ChoiceOnMap(discriminator, _TripAction);

    }

    private void _TripAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        var target = selectedTile.OccupiedUnit;
        if (target == null)
            return;
        AddLog($"{user.UnitName} trip {target.UnitName}\n");

        AddLog($"{user.unitData.GetSkill(Skills.Athletics)}(Athletics) +");
        int roll = Utils.CheckRoll();
        AddLog($" = {user.unitData.GetSkill(Skills.Athletics) + roll}\n");
        var outcome = Utils.CalculateOutCome(user.unitData.GetSkill(Skills.Athletics) + roll, 10 + target.unitData.GetReflexSave());
        AddLog("\n");


        ProneCondition condition = (ProneCondition)ScriptableObject.CreateInstance(typeof(ProneCondition));
        condition.duration = ConditionDuration.StartOfTurn;
        switch (outcome)
        {
            case (OutCome.CritSuccess):
                AddLog($"{target.UnitName} crashed to the groud\n");
                roll = Utils.d6();
                AddLog("\n");
                target.TakeDamage(roll);
                AddLog($"{target.UnitName} in now prone\n");
                target.AddCondition(condition);
                break;
            case (OutCome.Success):
                AddLog($"{target.UnitName} fell and is now prone\n");
                target.AddCondition(condition);
                break;
            case (OutCome.Fail):
                MenuManager.Instance.AddLog("You failed to trip the enemy, try again.\n");
                break;
            case (OutCome.CritFail):
                MenuManager.Instance.AddLog("You have fallen");
                if(user.Turns >= 1)
                {
                    MenuManager.Instance.AddLog(" and got up\n");
                    user.UpdateTurns(1);
                }
                else
                {
                    MenuManager.Instance.AddLog(" and now prone\n");
                    user.AddCondition(condition);
                }
                break;
        }
        user.UpdateTurns(1);
    }

    public void Feint()
    {
         
    }

    public void Demoralize()
    {
        if (!IsPlayersTurn()) return;
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= 1 + character.unitData.GetStat(Abilities.Charisma) && t.Value > 0;
        ChoiceOnMap(discriminator, _DemoralizeAction);

    }

    private void _DemoralizeAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        var target = selectedTile.OccupiedUnit;
        if (target == null)
            return;
        AddLog($"{user.UnitName} demoralize {target.UnitName}\n");

        AddLog($"{user.unitData.GetSkill(Skills.Intimidation)}(Intimidation) +");
        int roll = Utils.CheckRoll();
        AddLog($" = {user.unitData.GetSkill(Skills.Intimidation) + roll}\n");
        var outcome = Utils.CalculateOutCome(user.unitData.GetSkill(Skills.Intimidation) + roll, 10 + target.unitData.GetWillSave());
        AddLog("\n");

        FrightenedCondition condition = (FrightenedCondition)ScriptableObject.CreateInstance(typeof(FrightenedCondition));
        condition.duration = ConditionDuration.EndOfTurn;
        switch (outcome)
        {
            case (OutCome.CritSuccess):
                AddLog($"{target.UnitName} is drastically demoralized\n");
                condition.conditionLevel = 2;
                target.AddCondition(condition);
                break;
            case (OutCome.Success):
                AddLog($"{target.UnitName} is demoralized\n");
                condition.conditionLevel = 1;
                target.AddCondition(condition);
                break;
            case (OutCome.Fail):
            case (OutCome.CritFail):
                AddLog("You failed to scare the enemy, try again.\n");
                break;
        }
        user.UpdateTurns(1);
    }

    public void RecallKnowledge()
    {
        if (!IsPlayersTurn()) return;
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= 1 + character.unitData.GetStat(Abilities.Wisdom) && t.Value > 0;

        ChoiceOnMap(discriminator, _RecallKnowledge);

    }

    private void _RecallKnowledge(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        var target = selectedTile.OccupiedUnit;
        if (target == null)
            return;
        AddLog($"{user.UnitName} tries to remember information about {target.UnitName}\n");

        AddLog($"{user.unitData.GetSkill(Skills.Arcana)}(Arcana) +");
        int roll = Utils.CheckRoll();
        AddLog($" = {user.unitData.GetSkill(Skills.Arcana) + roll}\n");
        var outcome = Utils.CalculateOutCome(user.unitData.GetSkill(Skills.Arcana) + roll, 10 + target.unitData.GetSkill(Skills.Deception));
        AddLog("\n");

        
        switch (outcome)
        {
            case (OutCome.CritSuccess):
            case (OutCome.Success):
                AddLog($"{user.UnitName} remember some information about {target.UnitName}\n");
                target.RevealInfo();
                break;
            case (OutCome.Fail):
            case (OutCome.CritFail):
                AddLog("You failed to get knowledge, try again.\n");
                break;
        }
        user.UpdateTurns(1);
    }

    private bool IsPlayersTurn()
    {
        return CombatManager.Instance.isPlayersTurn();
    }

    // Wait for the player to select a tile and then call the given action with that tile
    public IEnumerator WaitForPlayerToSelect(IEnumerable<KeyValuePair<Tile, int>> tiles, Action<IEnumerable<KeyValuePair<Tile, int>>, BaseCharacter> action)
    {
        while (selectedTile == null)
            yield return null;
        action(tiles, character);
        // Hide the tiles that were highlighted.
        GridManager.Instance.ToggleOffDarkLightTiles();
    }

    public void ChoiceOnMap(Func<KeyValuePair<Tile, int>, bool> discriminator, Action<IEnumerable<KeyValuePair<Tile, int>>, BaseCharacter> action)
    {
        var tiles = GridManager.Instance.ToggleDarkLightTiles(discriminator, character.OccupiedTile);

        // Wait for player to select a tile and then move the character to the selected tile.
        StartCoroutine(WaitForPlayerToSelect(tiles, action));

    }

    private void AddLog(string log)
    {
        MenuManager.Instance.AddLog(log);
    }
}
