using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// This is a class named UnitManager that is inherited from MonoBehaviour.
public class UnitManager : MonoBehaviour
{
    // This is a public static instance of UnitManager that can be accessed from any other script.
    public static UnitManager Instance;

    // This is a private list of ScriptableUnits.
    private List<ScriptableUnit> _units;

    // This is a public field for the character.
    public BaseCharacter Character;

    // This is a public list of BaseEnemies.
    public List<BaseEnemy> Enemies;

    //[SerializeField] private Transform _cam;

    // This method is called when the script instance is being loaded.
    void Awake()
    {
        // Assign this instance to the public static Instance field.
        Instance = this;

        // Load all ScriptableUnits from the "Units" folder and convert to list.
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    //TODO: change function to random character. Make character singleton.
    // This method is used to spawn the player's character.
    public void SpawnCharacter()
    {
        // Set the number of characters to spawn.
        var characterCount = 1;

        // Spawn the characters.
        for (int i = 0; i < characterCount; i++)
        {
            // Get a random character prefab.
            var randomPrefab = GetRandomUnit<BaseCharacter>(Faction.Character);

            // Instantiate the character prefab.
            Character = Instantiate(randomPrefab);
            Character.init();

            RevealedInfoCondition cond = (RevealedInfoCondition)ScriptableObject.CreateInstance(typeof(RevealedInfoCondition));
            cond.duration = ConditionDuration.Custom;
            Character.AddCondition(cond);
            // Get a random spawn tile.
            var randomSpawnTile = GridManager.Instance.GetCharacterSpawnTile();

            // Set the character on the spawn tile.
            randomSpawnTile.SetUnit(Character);

            // Set up the menu for the character.
            MenuManager.Instance.setupMenu(Character);
        }

       // _cam.transform.parent = Character.transform;
        // Change the game state to spawn enemies.
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    // This method is used to spawn enemies.
    public void SpawnEnemies()
    {
        // Get the distance matrix.
        var distMatrix = BFS.GetDistanceMatrix(GridManager.Instance._tiles[Character.GetCurrentPosition()]);

        // Set the number of enemies to spawn.
        var enemyCount = 4;

        // Spawn the enemies.
        for (int i = 0; i < enemyCount; i++)
        {
            // Get a random enemy prefab.
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);

            // Instantiate the enemy prefab.
            var spawnedEnemy = Instantiate(randomPrefab);
            spawnedEnemy.init();

            // Get a random spawn tile.
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile(distMatrix);

            // Set the enemy on the spawn tile.
            randomSpawnTile.SetUnit(spawnedEnemy);

            // Add the enemy to the Enemies list.
            Enemies.Add(spawnedEnemy);
        }

        // Change the game state to the player's turn.
        GameManager.Instance.ChangeState(GameState.PlayersTurn);
    }

    public void PlayerTurn()
    {
        Character.ResetTurn();
    }

    // This coroutine executes the turn of each enemy in the Enemies list.
    public IEnumerator EnemiesTurn()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (BaseEnemy enemy in Enemies)
        {
            yield return enemy.EnemyTurn(); // Wait for the enemy to finish its turn.
        }

        GameManager.Instance.ChangeState(GameState.PlayersTurn); // Change the game state to player's turn after all enemies have taken their turns.
    }

    // This method returns a random unit of type T from the units list with the specified faction.
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
}