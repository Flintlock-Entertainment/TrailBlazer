using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponLogic : ItemLogic
{
    private static ScriptableWeapon weaponData;
    public static new WeaponLogic Instance;
    public override int Use(BaseEnemy user, ScriptableItem itemData)
    {
        weaponData = (ScriptableWeapon)itemData;
        // Get the distance matrix for the current tile and show tiles that are within the character's attack range.
        var distMatrix = BFS.GetDistanceMatrix(user.OccupiedTile);
        var tiles = distMatrix.Where(t => t.Value <= weaponData.GetRange());
        var target = UnitManager.Instance.Character;
        foreach (var tile in tiles)
        {
            if (tile.Key.OccupiedUnit == target)
            {
                Attack(user, target);
                break;
            }
        }
        return weaponData.GetNumOfActions();
    }
    /*
     * Perform the Attack action for the character.
     * Shows the tiles that the character can attack and waits for player input to select a tile.
     * Once a tile is selected, attacks any enemy units on the tile and ends the turn.
     */
    public override int Use(BaseCharacter user, ScriptableItem itemData)
    {
        weaponData = (ScriptableWeapon)itemData;
        // Get the distance matrix for the current tile and show tiles that are within the character's attack range.
        var distMatrix = BFS.GetDistanceMatrix(user.OccupiedTile);
        var tiles = distMatrix.Where(t => t.Value <= weaponData.GetRange());
        user.ToggleDarkLightTiles(tiles, true);
        // Wait for player to select a tile and then attack any enemy units on the tile.
        StartCoroutine(user.WaitForPlayerToSelect(tiles, _AttackAction));

        return weaponData.GetNumOfActions();
    }

    /*
     * Private function that actually performs the Attack action after a tile is selected.
     * Attacks any enemy units on the selected tile and hides the tiles that were highlighted.
     */
    private void _AttackAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        // Check if any enemy units are on the selected tile and attack them.
        if (user.selectedTile.OccupiedUnit != null && user.selectedTile.OccupiedUnit.Faction != Faction.Character)
        {
            var target = user.selectedTile.OccupiedUnit;
            Attack(user, target);
        }
    }

    private void Attack(BaseUnit user, BaseUnit target)
    {
        MenuManager.Instance.AddLog($"{user.UnitName} attacked {target.UnitName}\n");
        var outCome = Utils.CalculateOutCome(weaponData.GetAttackRoll(user), target.unitData.GetAC());
        int damage;
        switch (outCome)
        {
            case (OutCome.CritSuccess):
                damage = weaponData.GetCritSuccessDamage(user);
                break;
            case (OutCome.Success):
                damage = weaponData.GetSuccessDamage(user);
                break;
            case (OutCome.Fail):
                damage = weaponData.GetFailDamage(user);
                break;
            default:
                damage = weaponData.GetCritFailDamage(user);
                break;
        }
        target.TakeDamage(damage);
        user.CharacterAttackCounterIncrease();
    }

}

