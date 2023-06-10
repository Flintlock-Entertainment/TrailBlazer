using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= character.unitData.GetSpeed();

        ChoiceOnMap(discriminator, _StrideAction);
    }

    private void _StrideAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        selectedTile.SetUnit(character);
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
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= 1;
        ChoiceOnMap(discriminator, _TripAction);

    }

    private void _TripAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        var target = selectedTile.OccupiedUnit;
        var outcome = Utils.CalculateOutCome(character.unitData.GetSkill(Skills.Athletics) + Utils.CheckRoll(), 10 + target.unitData.GetReflexSave());
        ProneCondition condition = (ProneCondition)ScriptableObject.CreateInstance(typeof(ProneCondition));
        condition.duration = ConditionDuration.StartOfTurn;
        switch (outcome)
        {
            case (OutCome.CritSuccess):
                target.TakeDamage(Utils.d6());
                target.AddCondition(condition);
                break;
            case (OutCome.Success):
                target.AddCondition(condition);
                break;
            case (OutCome.CritFail):
                user.AddCondition(condition);
                user.UpdateTurns(1);
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
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= 1;
        ChoiceOnMap(discriminator, _DemoralizeAction);

    }

    private void _DemoralizeAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        var target = selectedTile.OccupiedUnit;
        var outcome = Utils.CalculateOutCome(character.unitData.GetSkill(Skills.Intimidation) + Utils.CheckRoll(), 10 + target.unitData.GetWillSave());
        FrightenedCondition condition = (FrightenedCondition)ScriptableObject.CreateInstance(typeof(FrightenedCondition));
        condition.duration = ConditionDuration.EndOfTurn;
        switch (outcome)
        {
            case (OutCome.CritSuccess):
                condition.conditionLevel = 2;
                target.AddCondition(condition);
                break;
            case (OutCome.Success):
                condition.conditionLevel = 1;
                target.AddCondition(condition);
                break;
        }
        user.UpdateTurns(1);
    }

    public void RecallKnowledge()
    {
        if (!IsPlayersTurn()) return;
        Func<KeyValuePair<Tile, int>, bool> discriminator = t => t.Value <= 1+ character.unitData.GetStat(Abilities.Wisdom);

        ChoiceOnMap(discriminator, _RecallKnowledge);

    }

    private void _RecallKnowledge(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        var target = selectedTile.OccupiedUnit;
        var outcome = Utils.CalculateOutCome(character.unitData.GetSkill(Skills.Arcana) + Utils.CheckRoll(), 10 + target.unitData.GetSkill(Skills.Deception));
        RevealedInfoCondition condition = (RevealedInfoCondition)ScriptableObject.CreateInstance(typeof(RevealedInfoCondition));
        condition.duration = ConditionDuration.Custom;
        switch (outcome)
        {
            case (OutCome.CritSuccess):
                target.AddCondition(condition);
                break;
            case (OutCome.Success):
                target.AddCondition(condition);
                break;
        }
        user.UpdateTurns(1);
    }

    private bool IsPlayersTurn()
    {
        return GameManager.Instance.isPlayersTurn();
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
}
