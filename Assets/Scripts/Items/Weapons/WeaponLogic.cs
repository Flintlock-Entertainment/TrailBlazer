using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// This class extends ItemLogic and represents a weapon item.
public class WeaponLogic : ItemLogic
{
    // This variable holds the data of the currently equipped weapon.
    private static ScriptableWeapon weaponData;
    // This variable holds the instance of WeaponLogic. The "new" keyword is used to hide the base class's Instance property.
    public static new WeaponLogic Instance;

    // This method is called when the weapon is used by an enemy unit.
    public override void Use(BaseEnemy user, ScriptableItem itemData)
    {
        // Cast the ScriptableItem to ScriptableWeapon and store it in weaponData.
        weaponData = (ScriptableWeapon)itemData;

        // Calculate the distance matrix from the user's current position using BFS algorithm.
        var distMatrix = BFS.GetDistanceMatrix(user.OccupiedTile);

        // Get all the tiles within the weapon's range.
        var tiles = distMatrix.Where(t => t.Value <= weaponData.GetRange());

        // Get the character unit if it is present on any of the tiles within the weapon's range.
        var target = UnitManager.Instance.Character;

        // Loop through the tiles within the weapon's range.
        foreach (var tile in tiles)
        {
            // If the character unit is present on the current tile, attack it and break the loop.
            if (tile.Key.OccupiedUnit == target)
            {
                Attack(user, target);
                break;
            }
        }
    }

    // This method is called when the weapon is used by a character unit.
    public override void Use(BaseCharacter user, ScriptableItem itemData)
    {
        // Cast the ScriptableItem to ScriptableWeapon and store it in weaponData.
        weaponData = (ScriptableWeapon)itemData;
        // Calculate the distance matrix from the user's current position using BFS algorithm.
        var distMatrix = BFS.GetDistanceMatrix(user.OccupiedTile);

        // Get all the tiles within the weapon's range.
        var tiles = distMatrix.Where(t => t.Value <= weaponData.GetRange());

        // Highlight all the tiles within the weapon's range.
        GridManager.Instance.ToggleDarkLightTiles(tiles);

        // Wait for the player to select a tile to attack.
        StartCoroutine(Actions.Instance.WaitForPlayerToSelect(tiles, _AttackAction));

    }

    // This method is called when the player selects a tile to attack.
    private void _AttackAction(IEnumerable<KeyValuePair<Tile, int>> tiles, BaseCharacter user)
    {
        // Check if the selected tile has an enemy unit and attack it.
        if (user.selectedTile.OccupiedUnit != null && user.selectedTile.OccupiedUnit.Faction != Faction.Character)
        {
            var target = user.selectedTile.OccupiedUnit;
            Attack(user, target);
        }
    }


    /* This method is responsible for handling the attack action of a weapon.
    * It takes in two BaseUnit parameters, the user and target of the attack.
    * It logs the attack action using the MenuManager, then calculates the outcome of the attack roll using Utils.CalculateOutCome method
    * The damage dealt to the target is then determined based on the outcome of the attack roll.
    * Finally, the target takes the calculated damage and the user's attack counter is increased.*/



    // This method performs the attack action.
    private void Attack(BaseUnit user, BaseUnit target)
    {
        // Add the attack log to the menu.
        MenuManager.Instance.AddLog($"{user.UnitName} attacked {target.UnitName}\n");

        // Calculate the outcome of the attack using the weapon's attack roll and the target's armor class.
        int roll = weaponData.GetAttackRoll(user);
        if (roll == -1)
            return;
        var outCome = Utils.CalculateOutCome(roll, target.unitData.GetAC());

        // Determine the damage dealt based on the outcome of the attack.
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

        // Apply the damage to the target and increase the user's attack counter
        target.TakeDamage(damage);
        user.CharacterAttackCounterIncrease();
        user.UpdateTurns(weaponData.GetNumOfActions());
    }
}